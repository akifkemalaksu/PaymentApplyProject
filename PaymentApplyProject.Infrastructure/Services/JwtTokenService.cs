using Microsoft.IdentityModel.Tokens;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Features.KullaniciFeatures.Authenticate;
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
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSettings jwtSettings;

        public JwtTokenService(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        public Response<AuthenticateResult> CreateToken(KullaniciDto kullaniciDto)
        {
            string guidString = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub,kullaniciDto.KullaniciAdi),
                new Claim(JwtRegisteredClaimNames.Jti,guidString),
                new Claim(JwtRegisteredClaimNames.Name,$"{kullaniciDto.Ad} {kullaniciDto.Soyad}"),
            };

            Parallel.ForEach(kullaniciDto.Yetkiler, (yetki) =>
                claims.Add(new Claim(ClaimTypes.Role, yetki))
                );

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

            var jsonToken = new AuthenticateResult
            {
                Token = tokenString,
                ValidTo = jwtSecurityToken.ValidTo
            };
            return Response<AuthenticateResult>.Success(System.Net.HttpStatusCode.OK, jsonToken);
        }
    }
}
