using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Status : BaseEntity<short>
    {
        [StringLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class WithdrawStatus : Status
    {
        public virtual ICollection<Withdraw> Withdraws { get; set; }
    }

    public class DepositStatus : Status
    {
        public virtual ICollection<Deposit> Deposits { get; set; }
    }
}
