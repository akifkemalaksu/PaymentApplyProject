using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class BankaHesabi : BaseEntity<int>
    {
        public int BankaId { get; set; }
        public string HesapNumarasi { get; set; }
        public bool OdemeHesabiMi { get; set; } // hint: AktifMi
    }

}
