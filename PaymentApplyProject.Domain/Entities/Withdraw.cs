using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Withdraw : BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public short WithdrawStatusId { get; set; }
        [StringLength(50)]
        public string AccountNumber { get; set; }
        public short BankId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        [StringLength(100)]
        public string ExternalTransactionId { get; set; }

        [StringLength(20)]
        public string MethodType { get; set; }
        public string CallbackUrl { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("WithdrawStatusId")]
        public virtual WithdrawStatus WithdrawStatus { get; set; }
    }

}
