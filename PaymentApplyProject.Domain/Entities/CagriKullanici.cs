using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class CagriKullanici : BaseEntity<int>
    {
        public int FirmaId { get; set; }
        public string KullaniciAdi { get; set; }
    }

}
