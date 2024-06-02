using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Domain.Entities
{

    public class User : BaseEntity<int>
    {
        [StringLength(20)]
        public string Username { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Password { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserCompany> UserCompanies { get; set; }
    }

}
