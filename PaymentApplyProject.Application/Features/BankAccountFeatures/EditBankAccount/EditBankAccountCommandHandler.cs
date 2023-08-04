using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.BankAccountFeatures.EditBankAccount;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.EditBankAccount
{
    public class EditBankAccountCommandHandler : IRequestHandler<EditBankAccountCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public EditBankAccountCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(EditBankAccountCommand request, CancellationToken cancellationToken)
        {
            var isExistSameAccountNumber = await _paymentContext.BankAccounts.AnyAsync(x =>
                x.Id != request.Id
                && x.AccountNumber == request.AccountNumber
                , cancellationToken);
            if (isExistSameAccountNumber)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniHesapNumarasinaSahipKayitVar);

            var bankAccount = _customMapper.Map<BankAccount>(request);
            _paymentContext.BankAccounts.Entry(bankAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
