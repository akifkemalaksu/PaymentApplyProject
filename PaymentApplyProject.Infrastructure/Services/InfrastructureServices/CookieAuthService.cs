using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;
using System.Security.Claims;

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
{
    public class CookieAuthService : ICookieAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieAuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task SignInAsync(UserDto kullaniciDto, bool rememberMe)
        {
            string guidString = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new(CustomClaimTypes.Username,kullaniciDto.Username),
                new(CustomClaimTypes.Name,kullaniciDto.Name),
                new(CustomClaimTypes.Surname,kullaniciDto.Surname),
                new(CustomClaimTypes.Email,kullaniciDto.Email),
                new(CustomClaimTypes.Id,kullaniciDto.Id.ToString()),
            };

            Parallel.ForEach(kullaniciDto.Roles, (yetki) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, yetki.Name));
                claims.Add(new Claim(CustomClaimTypes.RoleId, yetki.Id.ToString()));
            });

            Parallel.ForEach(kullaniciDto.Companies, (firma) =>
            {
                claims.Add(new Claim(CustomClaimTypes.CompanyId, firma.Id.ToString()));
                claims.Add(new Claim(CustomClaimTypes.Company, firma.Name));
            });

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

        public Task SignOutAsync()
        {
            return _httpContextAccessor.HttpContext.SignOutAsync();
        }
    }
}
