using System.ComponentModel.DataAnnotations;

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
