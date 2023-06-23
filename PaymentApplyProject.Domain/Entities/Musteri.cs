using PaymentApplyProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PaymentApplyProject.Domain.Entities
{
    public class Musteri : BaseEntity<int>
    {
        [NotNull]
        public short FirmaId { get; set; }
        [NotNull]
        [StringLength(50)]
        public string KullaniciAdi { get; set; }

        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set;}
    }

}
