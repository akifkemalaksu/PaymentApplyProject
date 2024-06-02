using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash
{
    public class GetDepositRequestFromHashQuery : IRequest<Response<GetDepositRequestFromHashResult>>
    {
        public required string HashKey { get; set; }
    }
}
