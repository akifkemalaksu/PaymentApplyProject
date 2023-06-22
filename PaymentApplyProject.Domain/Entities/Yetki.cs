using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Yetki : BaseEntity<byte>
    {
        public string Ad { get; set; }

        public virtual ICollection<KullaniciYetki> KullaniciYetkiler { get; set; }
    }
}
