using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class BankaHesabi : BaseEntity<int>
    {
        [NotNull]
        public short BankaId { get; set; }
        [NotNull]
        [StringLength(20)]
        public string HesapNumarasi { get; set; }
        [NotNull]
        public bool OdemeHesabiMi { get; set; } // hint: AktifMi

        [ForeignKey("BankaId")]
        public virtual Banka Banka { get; set; }
    }

}
