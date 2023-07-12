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
using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using PaymentApplyProject.Domain.Constants;
using Microsoft.AspNetCore.Http;
using PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken;
using System.Xml.Linq;

namespace PaymentApplyProject.Infrastructure.Services
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtAuthService(JwtSettings jwtSettings, IHttpContextAccessor httpContextAccessor)
        {
            _jwtSettings = jwtSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public Response<AuthenticateTokenResult> CreateToken(UserDto kullaniciDto)
        {
            string guidString = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,guidString),
                new Claim(JwtRegisteredClaimNames.Sub,kullaniciDto.Id.ToString()),

                new Claim(CustomClaimTypes.Username,kullaniciDto.Username),
                new Claim(CustomClaimTypes.Email,kullaniciDto.Email),
                new Claim(CustomClaimTypes.Name,kullaniciDto.Name),
                new Claim(CustomClaimTypes.Surname, kullaniciDto.Surname),
            };

            Parallel.ForEach(kullaniciDto.Roles, (yetki) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, yetki.Name));
                claims.Add(new Claim(CustomClaimTypes.RoleId, yetki.Id.ToString()));
            });

            Parallel.ForEach(kullaniciDto.Companies, (firma) =>
            {
                claims.Add(new Claim(CustomClaimTypes.Company, firma.Name));
                claims.Add(new Claim(CustomClaimTypes.CompanyId, firma.Id.ToString()));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(_jwtSettings.TokenTimeoutHours)),
                claims: claims,
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var jsonToken = new AuthenticateTokenResult
            {
                Token = tokenString,
                ValidTo = jwtSecurityToken.ValidTo
            };
            return Response<AuthenticateTokenResult>.Success(System.Net.HttpStatusCode.OK, jsonToken);
        }

        // todo: burada ek olarak yetkiler de gelmeli ve firmalar
        public UserDto GetSignedInUserInfos()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;

            if (claimsPrincipal == null) return null;

            var claims = claimsPrincipal.Claims;

            UserDto signedInUser = new()
            {
                Name = claims.First(x => x.Type == JwtRegisteredClaimNames.Name).Value,
                Surname = claims.First(x => x.Type == JwtRegisteredClaimNames.FamilyName).Value,
                Email = claims.First(x => x.Type == JwtRegisteredClaimNames.Email).Value,
                Username = claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value,
                Id = int.Parse(claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value),
            };
            return signedInUser;
        }
    }
}
