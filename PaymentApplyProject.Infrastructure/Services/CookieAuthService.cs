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

        public Task SignInAsync(UserDto kullaniciDto, bool rememberMe)
        {
            string guidString = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,kullaniciDto.Username),
                new Claim(ClaimTypes.Name,kullaniciDto.Name),
                new Claim(ClaimTypes.Surname,kullaniciDto.Surname),
                new Claim(ClaimTypes.Email,kullaniciDto.Email),
                new Claim(CustomClaimTypes.Id,kullaniciDto.Id.ToString()),
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

        public Task SignOutAsync() => _httpContextAccessor.HttpContext.SignOutAsync();

        // todo: burada ek olarak yetkiler de gelmeli ve firmalar
        public UserDto GetSignedInUserInfos()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;

            if (claimsPrincipal == null) return null;

            var claims = claimsPrincipal.Claims;

            UserDto signedInUser = new()
            {
                Name = claims.First(x => x.Type == ClaimTypes.Name).Value,
                Surname = claims.First(x => x.Type == ClaimTypes.Surname).Value,
                Email = claims.First(x => x.Type == ClaimTypes.Email).Value,
                Username = claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Id = int.Parse(claims.First(x => x.Type == CustomClaimTypes.Id).Value),
            };

            var companyNames = claims.Where(x => x.Type == CustomClaimTypes.Company).Select(x => x.Value).ToArray();
            var companyIds = claims.Where(x => x.Type == CustomClaimTypes.CompanyId).Select(x => x.Value).ToArray();
            var companyCount = companyNames.Count();
            for (int i = 0; i < companyCount; i++)
            {
                (signedInUser.Companies as List<CompanyDto>).Add(new CompanyDto
                {
                    Id = short.Parse(companyIds[i]),
                    Name = companyNames[i]
                });
            }

            var rolesNames = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
            var rolesIds = claims.Where(x => x.Type == CustomClaimTypes.RoleId).Select(x => x.Value).ToArray();
            var rolesCount = rolesNames.Count();
            for (int i = 0; i < rolesCount; i++)
            {
                (signedInUser.Roles as List<RoleDto>).Add(new RoleDto
                {
                    Id = short.Parse(rolesIds[i]),
                    Name = rolesNames[i]
                });
            }

            return signedInUser;
        }
    }
}
