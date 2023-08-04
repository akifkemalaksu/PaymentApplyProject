using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Helpers;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using System.Net.Http.Json;
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
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.NotAuthenticated);

            if (!userInfo.Companies.Any())
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.UserHasNoCompany);

            var companyId = userInfo.Companies.First().Id;
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotActive);

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.ExternalCustomerId == request.CustomerInfo.CustomerId && x.CompanyId == companyId && x.Deleted, cancellationToken);

            if (customer != null)
            {
                var isExistPendingDeposit = await _paymentContext.Deposits.AnyAsync(x => x.CustomerId == customer.Id && x.DepositStatusId == StatusConstants.DEPOSIT_BEKLIYOR && !x.Deleted, cancellationToken);
                if (isExistPendingDeposit)
                    return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.ThereIsPendingDeposit);

                if (!customer.Active)
                    return Response<DepositRequestResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotActive);
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
                UniqueTransactionIdHash = GeneratorHelper.GenerateSha256Key(request.UniqueTransactionId),
                CompanyId = companyId
            };

            await _paymentContext.DepositRequests.AddAsync(depositRequest, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var redirectUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/PaymentFrame/Panel/{uniqueTransactionIDHash}";
            var depositRequestResult = new DepositRequestResult
            {
                ExternalTransactionId = depositRequest.Id,
                RedirectUrl = redirectUrl
            };

            return Response<DepositRequestResult>.Success(System.Net.HttpStatusCode.OK, depositRequestResult);
        }
    }
}
