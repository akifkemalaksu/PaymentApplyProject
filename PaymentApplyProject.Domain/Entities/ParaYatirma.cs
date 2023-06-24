using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class ParaYatirma : BaseEntity<int>
    {
        [NotNull]
        public int MusteriId { get; set; }
        [NotNull]
        public short ParaYatirmaDurumId { get; set; }
        [NotNull]
        public int BankaHesapId { get; set; }
        [NotNull]
        public decimal Tutar { get; set; }
        public decimal OnaylananTutar { get; set; }

        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; }
        [ForeignKey("ParaYatirmaDurumId")]
        public virtual ParaYatirmaDurum ParaYatirmaDurum { get; set; }
        [ForeignKey("BankaHesabiId")]
        public virtual BankaHesabi BankaHesabi { get; set; }
    }
}
