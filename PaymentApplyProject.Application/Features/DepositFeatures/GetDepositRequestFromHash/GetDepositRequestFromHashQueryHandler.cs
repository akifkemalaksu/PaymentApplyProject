using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash
{
    public class GetDepositRequestFromHashQueryHandler : IRequestHandler<GetDepositRequestFromHashQuery, Response<GetDepositRequestFromHashResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetDepositRequestFromHashQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetDepositRequestFromHashResult>> Handle(GetDepositRequestFromHashQuery request, CancellationToken cancellationToken)
        {
            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.UniqueTransactionIdHash == request.HashKey && !x.Deleted);
            if (depositRequest == null)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.DepositRequestIsNotFound);

            var isExistsDeposit = await _paymentContext.Deposits.AnyAsync(x => x.DepositRequestId == depositRequest.Id && !x.Deleted, cancellationToken);
            if (isExistsDeposit)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.DepositRequestHashIsUsed);

            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == depositRequest.CompanyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotActive);

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.ExternalCustomerId == depositRequest.CustomerId && x.CompanyId == depositRequest.CompanyId && !x.Deleted, cancellationToken);

            if (customer == null)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CustomerIsNotFound);
            else if (!customer.Active)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CustomerIsNotActive);

            if (!depositRequest.ValidTo.HasValue)
            {
                depositRequest.ValidTo = DateTime.Now.AddMinutes(5);
                await _paymentContext.SaveChangesAsync(cancellationToken);
            }
            else if (depositRequest.ValidTo.HasValue && depositRequest.ValidTo.Value <= DateTime.Now)
                return Response<GetDepositRequestFromHashResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.DepositRequestIsTimeout);

            var getDepositRequestFromHashResult = new GetDepositRequestFromHashResult
            {
                CustomerId = customer.Id,
                FailedUrl = depositRequest.FailedUrl,
                SuccessUrl = depositRequest.SuccessUrl,
                DepositRequestId = depositRequest.Id,
                ValidTo = depositRequest.ValidTo.Value
            };

            return Response<GetDepositRequestFromHashResult>.Success(System.Net.HttpStatusCode.OK, getDepositRequestFromHashResult);
        }
    }
}
