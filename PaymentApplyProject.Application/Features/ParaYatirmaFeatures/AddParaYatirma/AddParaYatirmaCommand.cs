﻿using MediatR;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma
{
    public class AddParaYatirmaCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public string FirmaAdi { get; set; }
        public string Url { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriKullaniciAdi { get; set; }
        public int BankaHesapId { get; set; }
        public decimal Tutar { get; set; }
    }
}
