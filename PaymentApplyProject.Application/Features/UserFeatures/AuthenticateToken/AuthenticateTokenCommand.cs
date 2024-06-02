using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken
{
    public class AuthenticateTokenCommand : IRequest<Response<AuthenticateTokenResult>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
