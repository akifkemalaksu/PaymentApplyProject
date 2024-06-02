using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class Withdraw : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public short WithdrawStatusId { get; set; }
        [StringLength(50)]
        public required string AccountNumber { get; set; }
        public short BankId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        [StringLength(100)]
        public required string ExternalTransactionId { get; set; }

        [StringLength(20)]
        public required string MethodType { get; set; }
        public required string CallbackUrl { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        [ForeignKey(nameof(WithdrawStatusId))]
        public virtual WithdrawStatus? WithdrawStatus { get; set; }
        [ForeignKey(nameof(BankId))]
        public virtual Bank? Bank { get; set; }
    }

}
