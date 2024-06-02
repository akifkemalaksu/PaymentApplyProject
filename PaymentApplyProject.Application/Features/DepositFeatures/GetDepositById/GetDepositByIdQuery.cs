using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById
{
    public class GetDepositByIdQuery : IRequest<Response<GetDepositByIdResult>>
    {
        public int Id { get; set; }
    }
}
