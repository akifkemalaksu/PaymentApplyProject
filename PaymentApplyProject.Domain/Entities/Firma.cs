using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Firma : BaseEntity<short>
    {
        [NotNull]
        [StringLength(100)]
        public string Ad { get; set; }
        // todo: firmaların url'leri olabilir aktif pasif durumunda
        public virtual ICollection<Musteri> Musteriler { get; set; }
    }
}
