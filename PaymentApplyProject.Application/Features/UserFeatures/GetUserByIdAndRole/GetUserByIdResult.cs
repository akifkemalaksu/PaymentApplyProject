using PaymentApplyProject.Application.Dtos.UserDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole
{
    public class GetUserByIdResult
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public bool Active { get; set; }
        public RoleDto? Role { get; set; }
        public required IEnumerable<CompanyDto> Companies { get; set; }
    }
}
