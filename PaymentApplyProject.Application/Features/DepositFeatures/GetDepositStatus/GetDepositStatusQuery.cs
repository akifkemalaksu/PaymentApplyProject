using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus
{
    public class GetDepositStatusQuery : IRequest<Response<GetDepositStatusResult>>
    {
        public string TransactionId { get; set; }
    }
}
