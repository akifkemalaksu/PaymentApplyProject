using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
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
                new Claim(CustomClaimTypes.Id,kullaniciDto.Id.ToString()),
                new Claim(CustomClaimTypes.Username,kullaniciDto.KullaniciAdi),
                new Claim(ClaimTypes.Name,kullaniciDto.Ad),
                new Claim(ClaimTypes.Surname,kullaniciDto.Soyad),
                new Claim(ClaimTypes.Email,kullaniciDto.Email),
            };

            Parallel.ForEach(kullaniciDto.Yetkiler, (yetki) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, yetki.Ad));
                claims.Add(new Claim(CustomClaimTypes.RoleId, yetki.Id.ToString()));
            });

            Parallel.ForEach(kullaniciDto.Firmalar, (firma) =>
            {
                claims.Add(new Claim(CustomClaimTypes.CompanyId, firma.Id.ToString()));
                claims.Add(new Claim(CustomClaimTypes.Company, firma.Ad));
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

        public Task SignOutAsync() => _httpContextAccessor.HttpContext.SignOutAsync();

        // todo: bu metot için ayrı servis yazılabilir, ISignedInUserService
        public SignedInUserDto GetSignedInUserInfos()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;

            if (claimsPrincipal == null) return null;

            var claims = claimsPrincipal.Claims;

            SignedInUserDto signedInUser = new()
            {
                Ad = claims.First(x => x.Type == ClaimTypes.Name).Value,
                Soyad = claims.First(x => x.Type == ClaimTypes.Surname).Value,
                Email = claims.First(x => x.Type == ClaimTypes.Email).Value,
                KullaniciAdi = claims.First(x => x.Type == CustomClaimTypes.Username).Value,
                Id = int.Parse(claims.First(x => x.Type == CustomClaimTypes.Id).Value),
            };
            return signedInUser;
        }
    }
}
