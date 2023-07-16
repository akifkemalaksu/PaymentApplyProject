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
        public decimal Amount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public int IntegrationId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("WithdrawStatusId")]
        public virtual WithdrawStatus WithdrawStatus { get; set; }
    }

}
