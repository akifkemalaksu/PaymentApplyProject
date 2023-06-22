using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme
{
    public class AddParaCekmeCommand
    {
        public string FirmaAdi { get; set; }
        public string KullaniciAdi { get; set; }
        public decimal CekilecekTutar { get; set; }
    }
}
