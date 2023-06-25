using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.AddMusteri
{
    public class AddOrUpdateAndGetMusteriCommand : IRequest<Response<AddOrUpdateAndGetMusteriResult>>, ITransactional
    {
        public string FirmaKodu { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriKullaniciAdi { get; set; }
    }
}
