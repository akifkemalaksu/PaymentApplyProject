using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.EditBankAccount
{
    public class EditBankAccountCommand
    {
        public int Id { get; set; }
        public short BankaId { get; set; }
        public string HesapNumarasi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public decimal UstLimit { get; set; }
        public decimal AltLimit { get; set; }
        public bool AktifMi { get; set; }
    }
}
