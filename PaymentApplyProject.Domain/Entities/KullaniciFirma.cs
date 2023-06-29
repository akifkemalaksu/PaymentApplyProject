using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class KullaniciFirma : BaseEntity<int>
    {
        public int KullaniciId { get; set; }
        public short FirmaId { get; set; }

        [ForeignKey("KullaniciId")]
        public virtual Kullanici Kullanici { get; set; }
        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set; }
    }

}
