using PaymentApplyProject.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PaymentApplyProject.Domain.Entities
{
    public class Firma : BaseEntity<short>
    {
        [NotNull]
        [StringLength(100)]
        public string Ad { get; set; }

        public virtual ICollection<Musteri> Musteriler { get; set; }
    }
}
