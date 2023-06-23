using PaymentApplyProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PaymentApplyProject.Domain.Entities
{
    public class Yetki : BaseEntity<short>
    {
        [NotNull]
        [StringLength(50)]
        public string Ad { get; set; }

        public virtual ICollection<KullaniciYetki> KullaniciYetkiler { get; set; }
    }
}
