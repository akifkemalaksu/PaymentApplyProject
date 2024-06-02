using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface IJwtAuthService
    {
        public Response<AuthenticateTokenResult> CreateToken(UserDto kullaniciDto);
    }
}
