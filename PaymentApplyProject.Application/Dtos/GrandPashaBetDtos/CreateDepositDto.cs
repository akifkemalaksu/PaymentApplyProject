using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.GrandPashaBetDtos
{
    public class CreateDepositDto
    {
        public string KullaniciAdi { get; set; }
        public int ParaYatirmaTalepId { get; set; }
    }
}
