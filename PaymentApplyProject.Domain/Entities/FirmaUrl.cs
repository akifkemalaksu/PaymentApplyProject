using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PaymentApplyProject.Domain.Entities
{
    public class FirmaUrl : BaseEntity<int>
    {
        [NotNull]
        [StringLength(300)]
        public string Url { get; set; }
        [NotNull]
        public short FirmaId { get; set; }

        [ForeignKey("FirmaId")]
        public Firma Firma { get; set; }
    }
}
