using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.UserFeatures.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public int Id { get; set; }
    }
}
