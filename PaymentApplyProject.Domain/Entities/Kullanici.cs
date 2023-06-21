using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Core.Entities;

namespace PaymentApplyProject.Domain.Entities
{

    public class Kullanici : BaseEntity<int>
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }

}
