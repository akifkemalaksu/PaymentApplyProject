using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Yetki : BaseEntity<short>
    {
        [StringLength(50)]
        public string Ad { get; set; }

        public virtual ICollection<KullaniciYetki> KullaniciYetkiler { get; set; }
    }
}
