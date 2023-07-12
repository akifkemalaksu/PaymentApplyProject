using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForDatatable;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableQuery : DtParameters, IRequest<DtResult<LoadBanksForDatatableResult>>
    {
    }
}
