using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.LoadMusterilerForSelect
{
    public class LoadMusterilerForSelectQuery : SelectParameters, IRequest<SelectResult>
    {
        public int FirmaId { get; set; }
    }
}
