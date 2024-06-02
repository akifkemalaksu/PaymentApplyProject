using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken
{
    public class AuthenticateTokenCommand : IRequest<Response<AuthenticateTokenResult>>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
