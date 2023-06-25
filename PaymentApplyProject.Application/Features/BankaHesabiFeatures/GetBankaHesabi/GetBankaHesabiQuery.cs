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

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankaHesabi
{
    public class GetBankaHesabiQuery : IRequest<Response<GetBankaHesabiResult>>
    {
        public int BankaId { get; set; }
        public decimal Tutar { get; set; }
    }
}
