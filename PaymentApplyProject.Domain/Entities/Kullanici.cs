using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Domain.Entities
{

    public class Kullanici : BaseEntity<int>
    {
        [NotNull]
        [StringLength(20)]
        public string KullaniciAdi { get; set; }
        [NotNull]
        [StringLength(20)]
        public string Sifre { get; set; }
        [NotNull]
        [StringLength(50)]
        public string Ad { get; set; }
        [NotNull]
        [StringLength(50)]
        public string Soyad { get; set; }

        public virtual ICollection<KullaniciYetki> KullaniciYetkiler { get; set; }
    }

}
