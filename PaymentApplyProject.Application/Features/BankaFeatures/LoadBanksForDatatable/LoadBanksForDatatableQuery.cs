using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.BankaFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableQuery : DtParameters, IRequest<DtResult<LoadBanksForDatatableResult>>
    {
    }
}
