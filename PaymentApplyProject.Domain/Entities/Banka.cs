using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Banka : BaseEntity<short>
    {
        public int BankaId { get; set; }
        public string BankaAdi { get; set; }
    }

}
