using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class KullaniciYetki : BaseEntity<int>
    {
        public string KullaniciId { get; set; }
        public int YetkiId { get; set; }
    }

}
