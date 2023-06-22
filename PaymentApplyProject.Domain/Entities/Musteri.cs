using PaymentApplyProject.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentApplyProject.Domain.Entities
{
    public class Musteri : BaseEntity<int>
    {
        public int FirmaId { get; set; }
        public string KullaniciAdi { get; set; }

        [ForeignKey("FirmaId")]
        public virtual Firma Firma { get; set;}
    }

}
