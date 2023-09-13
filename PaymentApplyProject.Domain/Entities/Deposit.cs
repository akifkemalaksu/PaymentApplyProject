using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Deposit : BaseEntity<int>
    {
        public int DepositRequestId { get; set; }
        public int CustomerId { get; set; }
        public short DepositStatusId { get; set; }
        public int? BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }

        [ForeignKey("DepositRequestId")]
        public virtual DepositRequest DepositRequest { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("DepositStatusId")]
        public virtual DepositStatus DepositStatus { get; set; }
        [ForeignKey("BankAccountId")]
        public virtual BankAccount? BankAccount { get; set; }
    }
}
