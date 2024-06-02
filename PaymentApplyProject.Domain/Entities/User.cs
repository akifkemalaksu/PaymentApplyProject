using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Domain.Entities
{

    public class User : BaseEntity<int>
    {
        [StringLength(20)]
        public required string Username { get; set; }
        [StringLength(100)]
        public required string Email { get; set; }
        [StringLength(20)]
        public required string Password { get; set; }
        [StringLength(100)]
        public required string Name { get; set; }
        [StringLength(100)]
        public required string Surname { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<UserCompany>? UserCompanies { get; set; }
    }

}
