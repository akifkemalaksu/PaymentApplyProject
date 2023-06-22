using PaymentApplyProject.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class BankaHesabi : BaseEntity<int>
    {
        public int BankaId { get; set; }
        public string HesapNumarasi { get; set; }
        public bool OdemeHesabiMi { get; set; } // hint: AktifMi

        [ForeignKey("BankaId")]
        public virtual Banka Banka { get; set; }
    }

}
