using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PaymentApplyProject.Domain.Entities
{
    public class KullaniciYetki : BaseEntity<int>
    {
        public int KullaniciId { get; set; }
        public short YetkiId { get; set; }

        [ForeignKey("KullaniciId")]
        public virtual Kullanici Kullanici { get; set; }
        [ForeignKey("YetkiId")]
        public virtual Yetki Yetki { get; set; }
    }

}
