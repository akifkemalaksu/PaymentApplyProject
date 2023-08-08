using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus
{
    public class GetWithdrawStatusQueryHandler : IRequestHandler<GetWithdrawStatusQuery, Response<GetWithdrawStatusResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public GetWithdrawStatusQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<GetWithdrawStatusResult>> Handle(GetWithdrawStatusQuery request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();

            if (userInfo == null)
                return Response<GetWithdrawStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.NotAuthenticated);

            if (!userInfo.Companies.Any())
                return Response<GetWithdrawStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.UserHasNoCompany);

            var companyId = userInfo.Companies.First().Id;
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<GetWithdrawStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<GetWithdrawStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotActive);

            var withdraw = await _paymentContext.Withdraws.Where(x => x.ExternalTransactionId == request.TransactionId && x.Customer.CompanyId == companyId && !x.Deleted).Select(x => new GetWithdrawStatusResult
            {
                AccountNumber = x.AccountNumber,
                AddDate = x.AddDate,
                Amount = x.Amount,
                Bank = x.Bank.Name,
                CustomerId = x.Customer.ExternalCustomerId,
                ExternalTransactionId = x.ExternalTransactionId,
                MethodType = x.MethodType,
                Name = x.Customer.Name,
                Surname = x.Customer.Surname,
                TransactionDate = x.TransactionDate,
                Username = x.Customer.Username,
                WithdrawStatusId = x.WithdrawStatusId
            }).FirstOrDefaultAsync(cancellationToken);

            if (withdraw == null)
                return Response<GetWithdrawStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.WithdrawIsNotFound);

            return Response<GetWithdrawStatusResult>.Success(System.Net.HttpStatusCode.OK, withdraw);
        }
    }
}
