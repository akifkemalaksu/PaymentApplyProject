using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.DepositFeatures.LoadDepositsForDatatable;

namespace PaymentApplyProject.Application.Features.DepositFeatures.LoadDepositsForDatatable
{
    public class LoadDepositsForDatatableQuery : DtParameters, IRequest<DtResult<LoadDepositsForDatatableResult>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BankId { get; set; }
        public int BankAccountId { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
    }
}
