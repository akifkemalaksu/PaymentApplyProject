using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.WithdrawFeatures.LoadWithdrawsForDatatable;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.LoadWithdrawsForDatatable
{
    public class LoadWithdrawsForDatatableQuery : DtParameters, IRequest<DtResult<LoadWithdrawsForDatatableResult>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
    }
}
