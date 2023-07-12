using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForSelect
{
    public class LoadCompaniesForSelectQuery : SelectParameters, IRequest<SelectResult>
    {
    }
}
