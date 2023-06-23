﻿using MediatR;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme
{
    public class AddParaCekmeCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public string FirmaAdi { get; set; }
        public string KullaniciAdi { get; set; }
        public string HesapNumarasi { get; set; }
        public decimal Tutar { get; set; }
    }
}
