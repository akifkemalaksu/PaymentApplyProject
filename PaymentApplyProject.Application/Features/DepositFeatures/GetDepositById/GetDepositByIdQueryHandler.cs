using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById
{
    public class GetDepositByIdQueryHandler : IRequestHandler<GetDepositByIdQuery, Response<GetDepositByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetDepositByIdQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetDepositByIdResult>> Handle(GetDepositByIdQuery request, CancellationToken cancellationToken)
        {
            var paraYatirma = await _paymentContext.Deposits
                .AsNoTracking()
                .Where(x =>
                    x.Id == request.Id
                    && !x.Delete)
                .Select(x => new GetDepositByIdResult
                {
                    Bank = x.BankAccount.Bank.Name,
                    StatusId = x.DepositStatusId,
                    Status = x.DepositStatus.Name,
                    Company = x.Customer.Company.Name,
                    BankAccountNumber = x.BankAccount.AccountNumber,
                    BankAccountName = x.BankAccount.Name,
                    BankAccountSurname = x.BankAccount.Surname,
                    CustomerUsername = x.Customer.Username,
                    CustomerName = x.Customer.Name,
                    CustomerSurname = x.Customer.Surname,
                    ApprovedAmount = x.ApprovedAmount,
                    Amount = x.Amount,
                    TransactionDate = x.TransactionDate,
                    AddDate = x.AddDate,
                    Id = x.Id
                }).FirstOrDefaultAsync(cancellationToken);

            if (paraYatirma == null)
                return Response<GetDepositByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            return Response<GetDepositByIdResult>.Success(System.Net.HttpStatusCode.OK, paraYatirma);
        }
    }
}
