using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.UserFeatures.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
