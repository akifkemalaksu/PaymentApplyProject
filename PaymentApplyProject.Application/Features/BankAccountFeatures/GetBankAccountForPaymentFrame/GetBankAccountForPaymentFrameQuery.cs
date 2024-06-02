using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame
{
    public class GetBankAccountForPaymentFrameQuery : IRequest<Response<GetBankAccountForPaymentFrameResult>>
    {
        public short BankId { get; set; }
        public int DepositRequestId { get; set; }
    }
}
