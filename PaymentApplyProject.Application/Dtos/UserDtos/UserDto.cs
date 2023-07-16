using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos.UserDtos;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Dtos.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
        public IEnumerable<CompanyDto> Companies { get; set; }

        public UserDto()
        {
            Roles = new List<RoleDto>();
            Companies = new List<CompanyDto>();
        }

        public bool DoesHaveUserRole() => Roles.ToList().Any(x => x.Id == RoleConstants.USER_ID);
        public bool DoesHaveAdminRole() => Roles.ToList().Any(x => x.Id == RoleConstants.ADMIN_ID);
    }
}
