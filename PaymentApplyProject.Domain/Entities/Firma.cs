using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{
    public class Firma : BaseEntity<short>
    {
        [StringLength(20)]
        // todo: bizim tarafa kaydettiğiğiz firma kodu
        public string ResponseCode { get; set; }

        [StringLength(20)]
        // todo: bizim istek atmamız için karşı tarafın bizi kaydettiği entegre kod
        public string RequestCode { get; set; }

        [StringLength(100)]
        public string Ad { get; set; }
        public bool AktifMi { get; set; }
        public virtual ICollection<Musteri> Musteriler { get; set; }
    }
}
