using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Firma : BaseEntity<short>
    {
        public string Ad { get; set; }

        public virtual ICollection<Musteri> Musteriler { get; set; }
    }
}
