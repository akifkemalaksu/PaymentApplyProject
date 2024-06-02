using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.Login
{
    public class LoginCommand : IRequest<Response<NoContent>>
    {
        public required string EmailUsername { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
