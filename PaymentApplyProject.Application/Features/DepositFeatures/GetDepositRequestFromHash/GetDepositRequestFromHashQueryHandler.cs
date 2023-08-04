using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash
{
    public class GetDepositRequestFromHashQueryHandler : IRequestHandler<GetDepositRequestFromHashQuery, Response<GetDepositRequestFromHashResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public GetDepositRequestFromHashQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<GetDepositRequestFromHashResult>> Handle(GetDepositRequestFromHashQuery request, CancellationToken cancellationToken)
        {
            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.UniqueTransactionIdHash == request.HashKey && !x.Deleted);
            if (depositRequest == null)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.NotFound, string.Empty, ErrorCodes.DepositRequestIsNotFound);

            var isExistsDeposit = await _paymentContext.Deposits.AnyAsync(x => x.DepositRequestId == depositRequest.Id && !x.Deleted, cancellationToken);
            if (isExistsDeposit)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.NotFound, string.Empty, ErrorCodes.DepositRequestHashIsUsed);

            var userInfo = _authenticatedUserService.GetUserInfo();

            if (userInfo == null)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.NotAuthenticated);

            if (!userInfo.Companies.Any())
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.UserHasNoCompany);

            var companyId = userInfo.Companies.First().Id;
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotActive);

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.ExternalCustomerId == depositRequest.CustomerId && x.CompanyId == companyId && x.Deleted, cancellationToken);

            if (customer == null)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CustomerIsNotFound);
            else if (!customer.Active)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CustomerIsNotActive);

            var getDepositRequestFromHashResult = new GetDepositRequestFromHashResult
            {
                CustomerId = customer.Id,
                FailedUrl = depositRequest.FailedUrl,
                SuccessUrl = depositRequest.SuccessUrl,
                DepositRequestId = depositRequest.Id
            };

            return Response<GetDepositRequestFromHashResult>.Success(System.Net.HttpStatusCode.OK, getDepositRequestFromHashResult);
        }
    }
}
