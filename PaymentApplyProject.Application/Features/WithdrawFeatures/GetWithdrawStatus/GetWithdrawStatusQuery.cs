using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus
{
    public class GetWithdrawStatusQuery : IRequest<Response<GetWithdrawStatusResult>>
    {
        public string TransactionId { get; set; }
    }
}
