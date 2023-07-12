using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.BankAccountFeatures.DeleteBankAccount;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.DeleteBankAccount
{
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public DeleteBankAccountCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankAccount = await _paymentContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Delete, cancellationToken);

            if (bankAccount == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            _paymentContext.BankAccounts.Remove(bankAccount);

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
