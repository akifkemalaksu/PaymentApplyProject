using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Musteri : BaseEntity<int>
    {
        public short FirmaId { get; set; }
        [StringLength(50)]
        public string KullaniciAdi { get; set; }
        [StringLength(100)]
        public string Ad { get; set; }
        [StringLength(100)]
        public string Soyad { get; set; }
        public bool AktifMi { get; set; }

        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set; }
    }

}
