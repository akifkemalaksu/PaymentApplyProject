using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{

    public class Kullanici : BaseEntity<int>
    {
        [StringLength(20)]
        public string KullaniciAdi { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Sifre { get; set; }
        [StringLength(100)]
        public string Ad { get; set; }
        [StringLength(100)]
        public string Soyad { get; set; }
        public bool AktifMi { get; set; }
        public virtual ICollection<KullaniciYetki> KullaniciYetkiler { get; set; }
        public virtual ICollection<KullaniciFirma> KullaniciFirmalar { get; set; }
    }

}
