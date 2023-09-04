using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface ICookieAuthService
    {
        public Task SignInAsync(UserDto kullaniciDto, bool rememberMe);
        public Task SignOutAsync();
    }
}
