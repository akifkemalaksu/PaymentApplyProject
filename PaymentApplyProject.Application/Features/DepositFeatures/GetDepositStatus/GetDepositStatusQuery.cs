using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus
{
    public class GetDepositStatusQuery : IRequest<Response<GetDepositStatusResult>>
    {
        public required string TransactionId { get; set; }
    }
}
