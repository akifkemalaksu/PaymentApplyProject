using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Services
{
    public class CookieAuthService : ICookieAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieAuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task SignInAsync(KullaniciDto kullaniciDto, bool rememberMe)
        {
            string guidString = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,kullaniciDto.KullaniciAdi),
                new Claim(ClaimTypes.Email,kullaniciDto.Email),
            };

            Parallel.ForEach(kullaniciDto.Yetkiler, (yetki) =>
                claims.Add(new Claim(ClaimTypes.Role, yetki))
                );

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = rememberMe
            };

            return _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
