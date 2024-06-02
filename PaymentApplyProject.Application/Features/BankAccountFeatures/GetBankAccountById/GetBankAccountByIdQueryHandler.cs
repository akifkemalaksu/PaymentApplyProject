using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountById
{
    public class GetBankAccountByIdQueryHandler : IRequestHandler<GetBankAccountByIdQuery, Response<GetBankAccountByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _mapper;

        public GetBankAccountByIdQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<Response<GetBankAccountByIdResult>> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var bankAccountQuery = _paymentContext.BankAccounts
                .Where(x =>
                    x.Id == request.Id
                    && !x.Deleted);

            var bankAccount = await _mapper.QueryMap<GetBankAccountByIdResult>(bankAccountQuery)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return bankAccount is null
                ? Response<GetBankAccountByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi)
                : Response<GetBankAccountByIdResult>.Success(System.Net.HttpStatusCode.OK, bankAccount);
        }
    }
}
