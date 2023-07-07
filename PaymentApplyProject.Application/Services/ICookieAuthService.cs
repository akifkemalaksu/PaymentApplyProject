using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services
{
    public interface ICookieAuthService
    {
        public Task SignInAsync(KullaniciDto kullaniciDto, bool rememberMe);
        public Task SignOutAsync();

        public SignedInUserDto GetSignedInUserInfos();
    }
}
