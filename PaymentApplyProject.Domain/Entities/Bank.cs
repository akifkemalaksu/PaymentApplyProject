using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PaymentApplyProject.Domain.Entities
{
    public class Bank : BaseEntity<short>
    {
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<BankAccount> BankaAccounts { get; set; }
    }
}
