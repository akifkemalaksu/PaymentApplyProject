using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Deposit : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public short StatusId { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int IntegrationId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("StatusId")]
        public virtual DepositStatus DepositStatus { get; set; }
        [ForeignKey("BankAccountId")]
        public virtual BankAccount BankAccount { get; set; }
    }
}
