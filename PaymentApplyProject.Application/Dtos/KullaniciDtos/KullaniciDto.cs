using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.KullaniciDtos
{
    public class KullaniciDto
    {
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public IEnumerable<string> Yetkiler { get; set; }

        public KullaniciDto()
        {
            Yetkiler = Enumerable.Empty<string>();
        }
    }
}
