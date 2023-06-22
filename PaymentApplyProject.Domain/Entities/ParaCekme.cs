using PaymentApplyProject.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class ParaCekme : BaseEntity<int>
    {
        public int FirmaId { get; set; }
        public int MusteriId { get; set; }
        public int DurumId { get; set; }
        public string BankaHesapIban { get; set; }
        public decimal Tutar { get; set; }
        public decimal OnaylananTutar { get; set; }

        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set; }
        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; }
        [ForeignKey("DurumId")]
        public virtual Durum Durum { get; set; }
    }

}
