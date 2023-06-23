using MediatR;
using PaymentApplyProject.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.GetParaYatirmaById
{
    public class GetParaYatirmaByIdQuery:IRequest<Response<GetParaYatirmaByIdResult>>
    {
        public int Id { get; set; }
    }
}
