using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame
{
    public class GetBankAccountForPaymentFrameQueryHandler : IRequestHandler<GetBankAccountForPaymentFrameQuery, Response<GetBankAccountForPaymentFrameResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _mapper;

        public GetBankAccountForPaymentFrameQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<Response<GetBankAccountForPaymentFrameResult>> Handle(GetBankAccountForPaymentFrameQuery request, CancellationToken cancellationToken)
        {
            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.Id == request.DepositRequestId && !x.Deleted, cancellationToken);

            if (depositRequest == null)
                return Response<GetBankAccountForPaymentFrameResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.ParaYatirmaTalebiBulunamadi);

            var bankAccountQuery = _paymentContext.BankAccounts
                .AsNoTracking()
                .Where(x =>
                    x.BankId == request.BankId
                    && x.LowerLimit <= depositRequest.Amount
                    && x.UpperLimit >= depositRequest.Amount
                    && x.Active
                    && !x.Deleted);

            var bankAccount = await _mapper.QueryMap<GetBankAccountForPaymentFrameResult>(bankAccountQuery)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return bankAccount == null
                ? Response<GetBankAccountForPaymentFrameResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.BankaHesabiBulunamadi)
                : Response<GetBankAccountForPaymentFrameResult>.Success(System.Net.HttpStatusCode.OK, bankAccount);
        }
    }
}
