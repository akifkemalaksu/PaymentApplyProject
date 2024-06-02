using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResult>>
    {
        public int Id { get; set; }
    }
}
