using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

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

            if (paraCekme is null)
                return Response<GetWithdrawByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            return Response<GetWithdrawByIdResult>.Success(System.Net.HttpStatusCode.OK, paraCekme);
        }
    }
}
