using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Domain.Entities
{
    public class Company : BaseEntity<short>
    {
        [StringLength(100)]
        public string Name { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
