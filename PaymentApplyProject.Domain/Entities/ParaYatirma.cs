using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class ParaYatirma : BaseEntity<int>
    {
        public int MusteriId { get; set; }
        public short ParaYatirmaDurumId { get; set; }
        public int BankaHesabiId { get; set; }
        public decimal Tutar { get; set; }
        public decimal? OnaylananTutar { get; set; }
        public DateTime? IslemTarihi { get; set; }
        public int EntegrasyonId { get; set; }

        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; }
        [ForeignKey("ParaYatirmaDurumId")]
        public virtual ParaYatirmaDurum ParaYatirmaDurum { get; set; }
        [ForeignKey("BankaHesabiId")]
        public virtual BankaHesabi BankaHesabi { get; set; }
    }
}
