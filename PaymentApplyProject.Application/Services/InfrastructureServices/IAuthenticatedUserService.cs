using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface IAuthenticatedUserService
    {
        UserDto GetUserInfo();
        int GetUserId();
        string GetUsername();
        string GetBearerToken();
    }
}
