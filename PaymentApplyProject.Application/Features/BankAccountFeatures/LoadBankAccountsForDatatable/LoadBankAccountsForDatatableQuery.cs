using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForDatatable;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForDatatable
{
    public class LoadBankAccountsForDatatableQuery : DtParameters, IRequest<DtResult<LoadBankAccountsForDatatableResult>>
    {
        public short BankId { get; set; }
        public bool? Active { get; set; }
        public decimal Amount { get; set; }
    }
}
