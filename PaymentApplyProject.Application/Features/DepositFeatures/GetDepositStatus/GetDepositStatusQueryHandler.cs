using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus
{
    public class GetDepositStatusQueryHandler : IRequestHandler<GetDepositStatusQuery, Response<GetDepositStatusResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public GetDepositStatusQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<GetDepositStatusResult>> Handle(GetDepositStatusQuery request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();

            if (userInfo == null)
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.NotAuthenticated, ErrorCodes.NotAuthenticated);

            if (!userInfo.Companies.Any())
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.NotAuthenticated, ErrorCodes.UserHasNoCompany);

            var companyId = userInfo.Companies.First().Id;
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CompanyIsNotFound, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.CompanyIsNotActive, ErrorCodes.CompanyIsNotActive);

            var deposit = await _paymentContext.Deposits.Where(x => x.DepositRequest.UniqueTransactionId == request.TransactionId && x.Customer.CompanyId == companyId && !x.Deleted).Select(x => new GetDepositStatusResult
            {
                AddDate = x.AddDate,
                Amount = x.Amount,
                CustomerId = x.Customer.ExternalCustomerId,
                MethodType = x.DepositRequest.MethodType,
                Name = x.Customer.Name,
                Surname = x.Customer.Surname,
                TransactionDate = x.TransactionDate,
                UniqueTransactionId = request.TransactionId,
                ExternalTransactionId = x.DepositRequestId,
                Username = x.Customer.Username,
                DepositStatusId = x.DepositStatusId,
            }).FirstOrDefaultAsync(cancellationToken);

            if (deposit == null)
            {
                var depositRequest = await _paymentContext.DepositRequests.Where(x => x.UniqueTransactionId == request.TransactionId && x.CompanyId == companyId && !x.Deleted).Select(x => new GetDepositStatusResult
                {
                    AddDate = x.AddDate,
                    CustomerId = x.CustomerId,
                    MethodType = x.MethodType,
                    Name = x.Name,
                    Surname = x.Surname,
                    UniqueTransactionId = x.UniqueTransactionId,
                    ExternalTransactionId = x.Id,
                    Username = x.Username,
                    Amount = default,
                    DepositStatusId = StatusConstants.DEPOSIT_REDDEDILDI
                }).FirstOrDefaultAsync(cancellationToken);

                return depositRequest == null
                    ? Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.DepositIsNotFound, ErrorCodes.DepositIsNotFound)
                    : Response<GetDepositStatusResult>.Success(System.Net.HttpStatusCode.OK, depositRequest);
            }

            return Response<GetDepositStatusResult>.Success(System.Net.HttpStatusCode.OK, deposit);
        }
    }
}
