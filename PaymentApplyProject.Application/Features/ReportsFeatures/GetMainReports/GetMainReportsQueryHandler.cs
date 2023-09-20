using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Services.InfrastructureServices;
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
            x.BankAccount != null
            && (userInfo.DoesHaveUserRole() ? companyIds.Contains(x.Customer.CompanyId) : true)
            && !x.Deleted);

            var withdraws = _paymentContext.Withdraws.Where(x =>
            (userInfo.DoesHaveUserRole() ? companyIds.Contains(x.Customer.CompanyId) : true)
            && !x.Deleted);

            GetMainReportsResult result = new()
            {
                DepositAmounts = await deposits.SumAsync(x => x.Amount, cancellationToken),
                ApprovedDepositAmounts = await deposits.Where(x => x.DepositStatusId == StatusConstants.DEPOSIT_ONAYLANDI).SumAsync(x => x.Amount, cancellationToken),
                RejectedDepositAmounts = await deposits.Where(x => x.DepositStatusId == StatusConstants.DEPOSIT_REDDEDILDI).SumAsync(x => x.Amount, cancellationToken),
                PendingDepositAmounts = await deposits.Where(x => x.DepositStatusId == StatusConstants.DEPOSIT_BEKLIYOR).SumAsync(x => x.Amount, cancellationToken),
                WithdrawAmounts = await withdraws.SumAsync(x => x.Amount, cancellationToken),
                ApprovedWithdrawAmounts = await withdraws.Where(x => x.WithdrawStatusId == StatusConstants.WITHDRAW_ONAYLANDI).SumAsync(x => x.Amount, cancellationToken),
                RejectedWithdrawAmounts = await withdraws.Where(x => x.WithdrawStatusId == StatusConstants.WITHDRAW_REDDEDILDI).SumAsync(x => x.Amount, cancellationToken),
                PendingWithdrawAmounts = await withdraws.Where(x => x.WithdrawStatusId == StatusConstants.WITHDRAW_BEKLIYOR).SumAsync(x => x.Amount, cancellationToken),
            };

            return Response<GetMainReportsResult>.Success(System.Net.HttpStatusCode.OK, result);
        }
    }
}
