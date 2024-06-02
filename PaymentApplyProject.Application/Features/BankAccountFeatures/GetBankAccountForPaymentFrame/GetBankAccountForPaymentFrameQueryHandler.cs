using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame
{
    public class GetBankAccountForPaymentFrameQueryHandler : IRequestHandler<GetBankAccountForPaymentFrameQuery, Response<GetBankAccountForPaymentFrameResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetBankAccountForPaymentFrameQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetBankAccountForPaymentFrameResult>> Handle(GetBankAccountForPaymentFrameQuery request, CancellationToken cancellationToken)
        {
            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.Id == request.DepositRequestId && !x.Deleted, cancellationToken);

            if (depositRequest == null)
                return Response<GetBankAccountForPaymentFrameResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.ParaYatirmaTalebiBulunamadi);

            var bankAccount = await _paymentContext.BankAccounts
                .AsNoTracking()
                .Where(x =>
                    x.BankId == request.BankId
                    && x.LowerLimit <= depositRequest.Amount
                    && x.UpperLimit >= depositRequest.Amount
                    && x.Active
                    && !x.Deleted)
                .Select(x =>
                new GetBankAccountForPaymentFrameResult
                {
                    BankAccountId = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    AccountNumber = x.AccountNumber,
                }).FirstOrDefaultAsync(cancellationToken);

            if (bankAccount == null)
                return Response<GetBankAccountForPaymentFrameResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.BankaHesabiBulunamadi);

            return Response<GetBankAccountForPaymentFrameResult>.Success(System.Net.HttpStatusCode.OK, bankAccount);
        }
    }
}
