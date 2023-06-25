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
        [StringLength(50)]
        public string HesapNumarasi { get; set; }
        [NotNull]
        [StringLength(100)]
        public string Ad { get; set; }
        [NotNull]
        [StringLength(100)]
        public string Soyad { get; set; }
        [NotNull]
        public decimal UstLimit { get; set; }
        [NotNull]
        public decimal AltLimit { get; set; }
        [NotNull]
        public bool AktifMi { get; set; } // hint: AktifMi

        [ForeignKey("BankaId")]
        public virtual Banka Banka { get; set; }
    }

}
