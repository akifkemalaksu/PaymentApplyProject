using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Banka : BaseEntity<short>
    {
        public string Ad { get; set; }

        public virtual ICollection<BankaHesabi> BankaHesaplar { get; set; }
    }
}
