using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.NotificationDtos;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash;
using PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw
{
    public class AddWithdrawCommandHandler : IRequestHandler<AddWithdrawCommand, Response<AddWithdrawResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly INotificationService _notificationService;

        public AddWithdrawCommandHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService, INotificationService notificationService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
            _notificationService = notificationService;
        }

        public async Task<Response<AddWithdrawResult>> Handle(AddWithdrawCommand request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();

            if (userInfo == null)
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.NotAuthenticated, ErrorCodes.NotAuthenticated);

            if (!userInfo.Companies.Any())
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.UserHasNoCompany, ErrorCodes.UserHasNoCompany);

            var companyId = userInfo.Companies.First().Id;
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CompanyIsNotFound, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CompanyIsNotActive, ErrorCodes.CompanyIsNotActive);

            var isExistWithdrawWithSameTransactionId = await _paymentContext.Withdraws.AnyAsync(x => x.ExternalTransactionId == request.TransactionId && x.Customer.CompanyId == companyId && !x.Deleted, cancellationToken);
            if (isExistWithdrawWithSameTransactionId)
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsWithdrawSameTransactionId, ErrorCodes.ThereIsWithdrawSameTransactionId);


            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.ExternalCustomerId == request.CustomerInfo.CustomerId && x.CompanyId == companyId && !x.Deleted, cancellationToken);

            if (customer != null)
            {
                var isExistPendingWithdraw = await _paymentContext.Withdraws.AnyAsync(x => x.CustomerId == customer.Id && x.WithdrawStatusId == StatusConstants.WITHDRAW_BEKLIYOR && !x.Deleted, cancellationToken);
                if (isExistPendingWithdraw)
                    return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingWithdraw, ErrorCodes.ThereIsPendingWithdraw);

                if (!customer.Active)
                    return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CustomerIsNotActive, ErrorCodes.CustomerIsNotActive);
                else if (customer.Name != request.CustomerInfo.Name ||
                    customer.Surname != request.CustomerInfo.Surname ||
                    customer.Username != request.CustomerInfo.Username)
                {
                    customer.Name = request.CustomerInfo.Name;
                    customer.Surname = request.CustomerInfo.Surname;
                    customer.Username = request.CustomerInfo.Username;
                }
            }
            else
            {
                customer = new()
                {
                    Active = true,
                    CompanyId = companyId,
                    ExternalCustomerId = request.CustomerInfo.CustomerId,
                    Name = request.CustomerInfo.Name,
                    Surname = request.CustomerInfo.Surname,
                    Username = request.CustomerInfo.Username,
                };

                await _paymentContext.Customers.AddAsync(customer, cancellationToken);
                await _paymentContext.SaveChangesAsync(cancellationToken);
            }

            Withdraw withdraw = new()
            {
                CustomerId = customer.Id,
                Amount = request.Amount,
                AccountNumber = request.AccountNumber,
                WithdrawStatusId = StatusConstants.WITHDRAW_BEKLIYOR,
                ExternalTransactionId = request.TransactionId,
                BankId = request.BankId,
                CallbackUrl = request.CallbackUrl,
                MethodType = request.MethodType,
            };

            await _paymentContext.Withdraws.AddAsync(withdraw, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            NotificationDto notification = new()
            {
                Message = "Yeni para çekme talebi!",
                Path = "/payment/withdraws"
            };
            var usernames = await _paymentContext.Users.Where(x =>
                (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.ADMIN_ID && !ur.Deleted)
                || (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.USER_ID && !ur.Deleted)
                    && x.UserCompanies.Any(uc => uc.CompanyId == company.Id && !uc.Deleted)))
                && !x.Deleted
            ).Select(x => x.Username).ToListAsync(cancellationToken);
            _notificationService.CreateNotificationToSpecificUsers(usernames, notification, cancellationToken);

            var addWithdrawResult = new AddWithdrawResult
            {
                ExternalTransactionId = withdraw.Id,
                Status = StatusConstants.PENDING
            };
            return Response<AddWithdrawResult>.Success(System.Net.HttpStatusCode.OK, addWithdrawResult, Messages.TransactionSuccessful);
        }
    }
}
