using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountById
{
    public class GetBankAccountByIdQueryHandler : IRequestHandler<GetBankAccountByIdQuery, Response<GetBankAccountByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public GetBankAccountByIdQueryHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<GetBankAccountByIdResult>> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var bankAccount = await _paymentContext.BankAccounts
                .Where(x =>
                    x.Id == request.Id
                    && !x.Deleted)
                .Select(x => new GetBankAccountByIdResult
                {
                    Name = x.Name,
                    Active = x.Active,
                    LowerLimit = x.LowerLimit,
                    Bank = x.Bank.Name,
                    BankId = x.BankId,
                    AccountNumber = x.AccountNumber,
                    Id = request.Id,
                    Surname = x.Surname,
                    UpperLimit = x.UpperLimit
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (bankAccount is null)
                return Response<GetBankAccountByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            return Response<GetBankAccountByIdResult>.Success(System.Net.HttpStatusCode.OK, bankAccount);
        }
    }
}
