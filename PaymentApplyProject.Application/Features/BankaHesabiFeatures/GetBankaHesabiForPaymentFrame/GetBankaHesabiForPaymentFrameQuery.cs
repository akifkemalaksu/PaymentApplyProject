using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankaHesabiForPaymentFrame;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankaHesabiForPaymentFrame
{
    public class GetBankaHesabiForPaymentFrameQuery : IRequest<Response<GetBankaHesabiForPaymentFrameResult>>
    {
        public int BankaId { get; set; }
        public decimal Tutar { get; set; }
    }
}
