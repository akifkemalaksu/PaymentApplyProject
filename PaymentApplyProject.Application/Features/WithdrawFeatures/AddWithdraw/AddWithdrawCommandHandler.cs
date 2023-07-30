using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;

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
            var userInfos = _authenticatedUserService.GetUserInfo();
            if (userInfos == null || !userInfos.Companies.Any())
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            var company = userInfos.Companies.First();

            var musteri = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.CompanyId == company.Id && x.Username == request.Username && !x.Delete, cancellationToken);
            if (musteri == null)
            {
                musteri = new()
                {
                    CompanyId = company.Id,
                    Username = request.Username,
                    Name = request.Name,
                    Surname = request.Surname
                };
                await _paymentContext.Customers.AddAsync(musteri, cancellationToken);
                await _paymentContext.SaveChangesAsync(cancellationToken);
            }

            var isExistsParaCekme = await _paymentContext.Withdraws.AnyAsync(x =>
                    x.CustomerId == musteri.Id
                    && x.WithdrawStatusId == WithdrawStatusConstants.BEKLIYOR
                    && !x.Delete, cancellationToken);
            if (isExistsParaCekme)
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingTransaction);

            Withdraw addParaCekme = new()
            {
                CustomerId = musteri.Id,
                Amount = request.Amount,
                AccountNumber = request.AccountNumber,
                WithdrawStatusId = WithdrawStatusConstants.BEKLIYOR,
                IntegrationId = request.IntegrationId
            };

            await _paymentContext.Withdraws.AddAsync(addParaCekme, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            NotificationDto notification = new()
            {
                Message = "Yeni para çekme talebi!",
                Path = "/payment/withdraws"
            };
            var usernames = await _paymentContext.Users.Where(x =>
                (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.ADMIN_ID && !ur.Delete)
                || (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.USER_ID && !ur.Delete)
                    && x.UserCompanies.Any(uc => uc.CompanyId == company.Id && !uc.Delete)))
                && !x.Delete
            ).Select(x => x.Username).ToListAsync(cancellationToken);
            _notificationService.CreateNotificationToSpecificUsers(usernames, notification, cancellationToken);

            return Response<AddWithdrawResult>.Success(System.Net.HttpStatusCode.OK,
                new()
                {
                    WithdrawId = addParaCekme.Id
                },
                Messages.TransactionSuccessful
                );
        }
    }
}
