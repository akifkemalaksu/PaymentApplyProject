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
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public IEnumerable<YetkiDto> Yetkiler { get; set; }
        public IEnumerable<FirmaDto> Firmalar { get; set; }

        public KullaniciDto()
        {
            Yetkiler = new List<YetkiDto>();
            Firmalar = new List<FirmaDto>();
        }
    }
}
