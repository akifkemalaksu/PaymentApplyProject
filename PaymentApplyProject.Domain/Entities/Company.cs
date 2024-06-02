using PaymentApplyProject.Domain.Entities.Bases;
using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Domain.Entities
{
    public class Company : BaseEntity<short>
    {
        [StringLength(100)]
        public required string Name { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }
    }
}
