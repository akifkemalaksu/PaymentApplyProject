using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.LoadWithdrawsForDatatable
{
    public class LoadWithdrawsForDatatableQuery : DtParameters, IRequest<DtResult<LoadWithdrawsForDatatableResult>>
    {
        public int FirmaId { get; set; }
        public int MusteriId { get; set; }
        public int DurumId { get; set; }
    }
}
