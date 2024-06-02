using Microsoft.IdentityModel.Tokens;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtAuthService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public Response<AuthenticateTokenResult> CreateToken(UserDto kullaniciDto)
        {
            string guidString = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Jti,guidString),
                new(CustomClaimTypes.Id,kullaniciDto.Id.ToString()),
                new(CustomClaimTypes.Email,kullaniciDto.Email),
                new(CustomClaimTypes.Username,kullaniciDto.Username),
                new(CustomClaimTypes.Name,kullaniciDto.Name),
                new(CustomClaimTypes.Surname, kullaniciDto.Surname),
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
                expires: DateTime.Now.AddMinutes(_jwtSettings.TokenTimeoutMinutes),
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
