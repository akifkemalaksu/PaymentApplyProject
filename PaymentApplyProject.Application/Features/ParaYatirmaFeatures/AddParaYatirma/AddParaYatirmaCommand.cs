using MediatR;
using PaymentApplyProject.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma
{
    public class AddParaYatirmaCommand : IRequest<Response<NoContent>>
    {
        public string FirmaAdi { get; set; }
        public string KullaniciAdi { get; set; }
        public int BankaHesapId { get; set; }
        public decimal Tutar { get; set; }
    }
}
