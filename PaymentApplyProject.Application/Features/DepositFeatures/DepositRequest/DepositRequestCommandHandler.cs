﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Helpers;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;
using DepositRequestModel = PaymentApplyProject.Domain.Entities.DepositRequest;

namespace PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest
{
    public class DepositRequestCommandHandler : IRequestHandler<DepositRequestCommand, Response<DepositRequestResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepositRequestCommandHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService, IHttpContextAccessor httpContextAccessor)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<DepositRequestResult>> Handle(DepositRequestCommand request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();

            if (userInfo == null)
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.NotAuthenticated, ErrorCodes.NotAuthenticated);

            if (!userInfo.Companies.Any())
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.UserHasNoCompany, ErrorCodes.UserHasNoCompany);

            var companyId = userInfo.Companies.First().Id;
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CompanyIsNotFound, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CompanyIsNotActive, ErrorCodes.CompanyIsNotActive);

            var isExistDepositWithSameTransactionId = await _paymentContext.DepositRequests.AnyAsync(x => x.UniqueTransactionId == request.UniqueTransactionId && x.CompanyId == companyId && !x.Deleted, cancellationToken);
            if (isExistDepositWithSameTransactionId)
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsDepositSameTransactionId, ErrorCodes.ThereIsDepositSameTransactionId);

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.ExternalCustomerId == request.CustomerInfo.CustomerId && x.CompanyId == companyId && !x.Deleted, cancellationToken);

            if (customer != null)
            {
                var isExistPendingDeposit = await _paymentContext.Deposits.AnyAsync(x => x.CustomerId == customer.Id && x.DepositStatusId == StatusConstants.DEPOSIT_BEKLIYOR && !x.Deleted, cancellationToken);
                if (isExistPendingDeposit)
                    return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingDeposit, ErrorCodes.ThereIsPendingDeposit);

                if (!customer.Active)
                    return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CustomerIsNotActive, ErrorCodes.CustomerIsNotActive);
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

            var uniqueTransactionIDHash = GeneratorHelper.GenerateSha256Key(request.UniqueTransactionId);
            DepositRequestModel depositRequest = new()
            {
                CallbackUrl = request.CallbackUrl,
                FailedUrl = request.FailedUrl,
                MethodType = request.MethodType,
                CustomerId = request.CustomerInfo.CustomerId,
                Name = request.CustomerInfo.Name,
                Surname = request.CustomerInfo.Surname,
                Username = request.CustomerInfo.Username,
                SuccessUrl = request.SuccessUrl,
                UniqueTransactionId = request.UniqueTransactionId,
                UniqueTransactionIdHash = uniqueTransactionIDHash,
                CompanyId = companyId,
                Amount = request.Amount,
            };

            await _paymentContext.DepositRequests.AddAsync(depositRequest, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            var hostAddress = _httpContextAccessor.HttpContext.Request.Host;
            var redirectUrl = $"https://{hostAddress}/PaymentFrame/Panel/{uniqueTransactionIDHash}";
            var depositRequestResult = new DepositRequestResult
            {
                ExternalTransactionId = depositRequest.Id,
                RedirectUrl = redirectUrl
            };

            return Response<DepositRequestResult>.Success(System.Net.HttpStatusCode.OK, depositRequestResult);
        }
    }
}
