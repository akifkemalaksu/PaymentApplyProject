using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById
{
    public class GetWithdrawByIdQuery : IRequest<Response<GetWithdrawByIdResult>>
    {
        public int Id { get; set; }
    }
}
