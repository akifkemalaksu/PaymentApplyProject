using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;

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
            var bankAccount = await _paymentContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (bankAccount is null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.BankaHesabiBulunamadi);

            var isExistSameAccountNumber = await _paymentContext.BankAccounts.AnyAsync(x =>
                x.Id != request.Id
                && x.BankId == request.BankId
                && x.AccountNumber == request.AccountNumber
                && !x.Deleted
                , cancellationToken);
            if (isExistSameAccountNumber)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AyniHesapNumarasinaSahipKayitVar);

            bankAccount.BankId = request.BankId;
            bankAccount.Name = request.Name;
            bankAccount.Surname = request.Surname;
            bankAccount.AccountNumber = request.AccountNumber;
            bankAccount.Active = request.Active;
            bankAccount.LowerLimit = request.LowerLimit;
            bankAccount.UpperLimit = request.UpperLimit;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
