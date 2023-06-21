using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Durum : BaseEntity<short>
    {
        public string Ad { get; set; }
        public string Aciklama { get; set; }
    }

}
