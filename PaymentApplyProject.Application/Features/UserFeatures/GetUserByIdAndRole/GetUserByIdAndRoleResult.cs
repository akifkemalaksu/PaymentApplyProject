using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole
{
    public class GetUserByIdAndRoleResult
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Active { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
        public IEnumerable<CompanyDto> Companies { get; set; }
    }
}
