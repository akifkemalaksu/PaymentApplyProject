using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;

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
                    && !x.Deleted)
                .Select(x => new GetWithdrawByIdResult
                {
                    AccountNumber = x.AccountNumber,
                    Bank = x.Bank.Name,
                    Status = x.WithdrawStatus.Name,
                    StatusId = x.WithdrawStatusId,
                    Company = x.Customer.Company.Name,
                    Username = x.Customer.Username,
                    Name = x.Customer.Name,
                    Surname = x.Customer.Surname,
                    Amount = x.Amount,
                    AddDate = x.AddDate,
                    Id = x.Id,
                    TransactionDate = x.TransactionDate,
                    ExternalTransactionId = x.ExternalTransactionId
                }).FirstOrDefaultAsync(cancellationToken);

            return paraCekme is null
                ? Response<GetWithdrawByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi)
                : Response<GetWithdrawByIdResult>.Success(System.Net.HttpStatusCode.OK, paraCekme);
        }
    }
}
