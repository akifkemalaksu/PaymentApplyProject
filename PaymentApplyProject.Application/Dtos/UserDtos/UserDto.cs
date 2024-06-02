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
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public IEnumerable<CompanyDto> Companies { get; set; } = new List<CompanyDto>();

        public bool Active { get; set; }

        public bool DoesHaveUserRole()
        {
            return Roles.ToList().Any(x => x.Id == RoleConstants.USER_ID);
        }

        public bool DoesHaveAdminRole()
        {
            return Roles.ToList().Any(x => x.Id == RoleConstants.ADMIN_ID);
        }

        public bool DoesHaveAccountingRole()
        {
            return Roles.ToList().Any(x => x.Id == RoleConstants.ACCOUNTING_ID);
        }
    }
}
