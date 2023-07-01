using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.ApproveParaYatirma
{
    public class ApproveParaYatirmaCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public decimal Tutar { get; set; }
    }
}
