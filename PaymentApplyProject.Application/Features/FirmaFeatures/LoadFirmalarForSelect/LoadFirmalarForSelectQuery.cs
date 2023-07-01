using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.FirmaFeatures.LoadFirmalarForSelect
{
    public class LoadFirmalarForSelectQuery : SelectParameters, IRequest<SelectResult>
    {
    }
}
