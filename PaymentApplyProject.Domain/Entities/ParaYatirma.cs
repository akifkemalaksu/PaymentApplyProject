using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class ParaYatirma : BaseEntity<int>
    {
        public int FirmaId { get; set; }
        public int CagriTuruId { get; set; }
        public int CagriKullaniciId { get; set; }
        public int DurumId { get; set; }
        public int BankaHesapId { get; set; }
        public decimal Tutar { get; set; }
        public decimal OnaylananTutar { get; set; }
    }

}
