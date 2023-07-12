using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

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
