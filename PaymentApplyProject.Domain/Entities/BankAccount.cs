using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class BankAccount : BaseEntity<int>
    {
        public short BankId { get; set; }
        [StringLength(50)]
        public string AccountNumber { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Surname { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public bool Active { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank Bank { get; set; }
    }

}
