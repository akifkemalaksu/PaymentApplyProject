using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.AddBankAccount
{
    public class AddBankAccountCommand : IRequest<Response<NoContent>>
    {
        public short BankaId { get; set; }
        public string HesapNumarasi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public decimal UstLimit { get; set; }
        public decimal AltLimit { get; set; }
    }
}
