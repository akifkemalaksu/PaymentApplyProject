using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface ICookieAuthService
    {
        public Task SignInAsync(UserDto kullaniciDto, bool rememberMe);
        public Task SignOutAsync();
    }
}
