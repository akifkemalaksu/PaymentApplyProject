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
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable
{
    public class LoadBankAccountsForDatatableQuery : DtParameters, IRequest<DtResult<LoadBankAccountsForDatatableResult>>
    {
        public short BankaId { get; set; }
        public bool? AktifMi { get; set; }
        public decimal Tutar { get; set; }
    }
}
