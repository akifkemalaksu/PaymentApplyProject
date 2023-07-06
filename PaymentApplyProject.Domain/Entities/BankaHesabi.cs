using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class BankaHesabi : BaseEntity<int>
    {
        public short BankaId { get; set; }
        [StringLength(50)]
        public string HesapNumarasi { get; set; }
        [StringLength(100)]
        public string Ad { get; set; }
        [StringLength(100)]
        public string Soyad { get; set; }
        public decimal UstLimit { get; set; }
        public decimal AltLimit { get; set; }
        public bool AktifMi { get; set; }

        [ForeignKey("BankaId")]
        public virtual Banka Banka { get; set; }
    }

}
