using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class ParaCekme : BaseEntity<int>
    {
        public int FirmaId { get; set; }
        public int CagriTuruId { get; set; }
        public int CagriKullaniciId { get; set; }
        public int DurumId { get; set; }
        public string BankaHesapIban { get; set; }
        public decimal Tutar { get; set; }
        public decimal OnaylananTutar { get; set; }
    }

}
