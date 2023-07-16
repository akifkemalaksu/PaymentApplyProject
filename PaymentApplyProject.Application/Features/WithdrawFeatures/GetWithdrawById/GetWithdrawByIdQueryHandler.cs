using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById
{
    public class GetWithdrawByIdQueryHandler : IRequestHandler<GetWithdrawByIdQuery, Response<GetWithdrawByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetWithdrawByIdQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetWithdrawByIdResult>> Handle(GetWithdrawByIdQuery request, CancellationToken cancellationToken)
        {
            var paraCekme = await _paymentContext.Withdraws
                .AsNoTracking()
                .Where(x =>
                    x.Id.Equals(request.Id)
                    && !x.Delete)
                .Select(x => new GetWithdrawByIdResult
                {
                    AccountNumber = x.AccountNumber,
                    Status = x.WithdrawStatus.Name,
                    StatusId = x.WithdrawStatusId,
                    Company = x.Customer.Company.Name,
                    Username = x.Customer.Username,
                    Name = x.Customer.Name,
                    Surname = x.Customer.Surname,
                    ApprovedAmount = x.ApprovedAmount,
                    Amount = x.Amount,
                    AddDate = x.AddDate,
                    Id = x.Id,
                    TransactionDate = x.TransactionDate,
                }).FirstOrDefaultAsync(cancellationToken);

            if (paraCekme is null)
                return Response<GetWithdrawByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetWithdrawByIdResult>.Success(System.Net.HttpStatusCode.OK, paraCekme);
        }
    }
}
