using System.ComponentModel.DataAnnotations;

namespace PaymentApplyProject.Domain.Entities
{
    public class Bank : BaseEntity<short>
    {
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<BankAccount> BankaAccounts { get; set; }
    }
}
