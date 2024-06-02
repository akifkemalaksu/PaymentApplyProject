using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Domain.Entities
{
    public class Role : BaseEntity<short>
    {
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
