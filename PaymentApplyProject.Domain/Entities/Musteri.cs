using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Musteri : BaseEntity<int>
    {
        [NotNull]
        public short FirmaId { get; set; }
        [NotNull]
        [StringLength(50)]
        public string KullaniciAdi { get; set; }
        [NotNull]
        [StringLength(50)]
        public string Ad { get; set; }
        [NotNull]
        [StringLength(50)]
        public string Soyad { get; set; }
        [NotNull]
        public bool AktifMi { get; set; }

        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set; }
    }

}
