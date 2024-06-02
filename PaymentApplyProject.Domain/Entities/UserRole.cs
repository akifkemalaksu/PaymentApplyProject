using PaymentApplyProject.Domain.Entities.Bases;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class UserRole : BaseEntity<int>
    {
        public int UserId { get; set; }
        public short RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }
        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; set; }
    }

}
