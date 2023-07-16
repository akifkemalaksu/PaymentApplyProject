using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.ReportsFeatures.GetMainReports
{
    public class GetMainReportsQueryHandler : IRequestHandler<GetMainReportsQuery, Response<GetMainReportsResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public GetMainReportsQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<GetMainReportsResult>> Handle(GetMainReportsQuery request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();
            var companyIds = userInfo.Companies.Select(x => x.Id).ToList();

            var deposits = _paymentContext.Deposits.Where(x =>
            (userInfo.DoesHaveUserRole() ? companyIds.Contains(x.Customer.CompanyId) : true)
            && !x.Delete);

            var withdraws = _paymentContext.Withdraws.Where(x =>
            (userInfo.DoesHaveUserRole() ? companyIds.Contains(x.Customer.CompanyId) : true)
            && !x.Delete);

            GetMainReportsResult result = new()
            {
                DepositAmounts = await deposits.SumAsync(x => x.Amount, cancellationToken),
                ApprovedDepositAmounts = await deposits.Where(x => x.DepositStatusId == DepositStatusConstants.ONAYLANDI).SumAsync(x => x.ApprovedAmount.Value, cancellationToken),
                RejectedDepositAmounts = await deposits.Where(x => x.DepositStatusId == DepositStatusConstants.REDDEDILDI).SumAsync(x => x.Amount, cancellationToken),
                PendingDepositAmounts = await deposits.Where(x => x.DepositStatusId == DepositStatusConstants.BEKLIYOR).SumAsync(x => x.Amount, cancellationToken),
                WithdrawAmounts = await withdraws.SumAsync(x => x.Amount, cancellationToken),
                ApprovedWithdrawAmounts = await withdraws.Where(x => x.WithdrawStatusId == WithdrawStatusConstants.ONAYLANDI).SumAsync(x => x.ApprovedAmount.Value, cancellationToken),
                RejectedWithdrawAmounts = await withdraws.Where(x => x.WithdrawStatusId == WithdrawStatusConstants.REDDEDILDI).SumAsync(x => x.Amount, cancellationToken),
                PendingWithdrawAmounts = await withdraws.Where(x => x.WithdrawStatusId == WithdrawStatusConstants.BEKLIYOR).SumAsync(x => x.Amount, cancellationToken),
            };

            return Response<GetMainReportsResult>.Success(System.Net.HttpStatusCode.OK, result);
        }
    }
}
