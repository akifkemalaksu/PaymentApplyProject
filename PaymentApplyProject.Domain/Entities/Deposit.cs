using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey(nameof(DepositRequestId))]
        public virtual DepositRequest? DepositRequest { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        [ForeignKey(nameof(DepositStatusId))]
        public virtual DepositStatus? DepositStatus { get; set; }
        [ForeignKey(nameof(BankAccountId))]
        public virtual BankAccount? BankAccount { get; set; }
    }
}
