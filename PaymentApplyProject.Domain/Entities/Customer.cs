using PaymentApplyProject.Domain.Entities.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class Customer : BaseEntity<int>
    {
        public short CompanyId { get; set; }
        [StringLength(50)]
        public required string Username { get; set; }
        [StringLength(100)]
        public required string Name { get; set; }
        [StringLength(100)]
        public required string Surname { get; set; }
        [StringLength(100)]
        public required string ExternalCustomerId { get; set; }
        public bool Active { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }
    }

}
