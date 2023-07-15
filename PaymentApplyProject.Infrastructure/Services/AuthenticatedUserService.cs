using Microsoft.AspNetCore.Http;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Infrastructure.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserDto GetUserInfo()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;

            if (claimsPrincipal == null) return null;

            var claims = claimsPrincipal.Claims;

            UserDto signedInUser = new()
            {
                Id = int.Parse(claims.First(x => x.Type == CustomClaimTypes.Id).Value),
                Username = claims.First(x => x.Type == CustomClaimTypes.Username).Value,
                Email = claims.First(x => x.Type == CustomClaimTypes.Email).Value,
                Name = claims.First(x => x.Type == CustomClaimTypes.Name).Value,
                Surname = claims.First(x => x.Type == CustomClaimTypes.Surname).Value,
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
