using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class ParaCekme : BaseEntity<int>
    {
        public int MusteriId { get; set; }
        public short ParaCekmeDurumId { get; set; }
        [StringLength(50)]
        public string HesapNumarasi { get; set; }
        public decimal Tutar { get; set; }
        public decimal? OnaylananTutar { get; set; }
        public DateTime? OnayRedTarihi { get; set; }
        public int EntegrasyonId { get; set; }

        [ForeignKey("MusteriId")]
        public virtual Musteri Musteri { get; set; }
        [ForeignKey("ParaCekmeDurumId")]
        public virtual ParaCekmeDurum ParaCekmeDurum { get; set; }
    }

}
