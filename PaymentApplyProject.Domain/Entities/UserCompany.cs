using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class UserCompany : BaseEntity<int>
    {
        public int UserId { get; set; }
        public short CompanyId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }

}
