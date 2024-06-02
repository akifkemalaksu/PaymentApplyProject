using PaymentApplyProject.Domain.Entities.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class DepositRequest : BaseEntity<int>
    {
        public short CompanyId { get; set; }
        public required string CallbackUrl { get; set; }
        public required string SuccessUrl { get; set; }
        public required string FailedUrl { get; set; }
        [StringLength(100)]
        public required string CustomerId { get; set; }
        [StringLength(100)]
        public required string Name { get; set; }
        [StringLength(100)]
        public required string Surname { get; set; }
        [StringLength(100)]
        public required string Username { get; set; }
        [StringLength(20)]
        public required string MethodType { get; set; }
        [StringLength(100)]
        public required string UniqueTransactionId { get; set; }
        [StringLength(64)]
        public required string UniqueTransactionIdHash { get; set; }
        public decimal Amount { get; set; }
        public DateTime? ValidTo { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }

        public virtual Deposit? Deposit { get; set; }
    }
}
