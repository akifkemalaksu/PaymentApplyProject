using System.ComponentModel.DataAnnotations;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class BaseEntity<T> : IBaseEntity<T>
        where T : notnull
    {
        [Key]
        public T Id { get; set; }
        public virtual bool SilindiMi { get; set; }
        public virtual int EkleyenKullaniciId { get; set; }
        public virtual int DuzenleyenKullaniciId { get; set; }
        public virtual DateTime EklemeTarihi { get; set; }
        public virtual DateTime GuncellemeTarihi { get; set; }
    }

}
