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
using PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Infrastructure.Services
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly JwtSettings jwtSettings;

        public JwtAuthService(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public Response<AuthenticateTokenResult> CreateToken(KullaniciDto kullaniciDto)
        {
            string guidString = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,guidString),
                new Claim(JwtRegisteredClaimNames.Sub,kullaniciDto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,kullaniciDto.KullaniciAdi),
                new Claim(JwtRegisteredClaimNames.Email,kullaniciDto.Email),
                new Claim(JwtRegisteredClaimNames.Name,kullaniciDto.Ad),
                new Claim(JwtRegisteredClaimNames.FamilyName, kullaniciDto.Soyad),
            };

            Parallel.ForEach(kullaniciDto.Yetkiler, (yetki) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, yetki.Ad));
                claims.Add(new Claim(CustomClaimTypes.RoleId, yetki.Id.ToString()));
            });

            Parallel.ForEach(kullaniciDto.Firmalar, (firma) =>
            {
                claims.Add(new Claim(CustomClaimTypes.Company, firma.Ad));
                claims.Add(new Claim(CustomClaimTypes.CompanyId, firma.Id.ToString()));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(jwtSettings.TokenTimeoutHours)),
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
    }
}
