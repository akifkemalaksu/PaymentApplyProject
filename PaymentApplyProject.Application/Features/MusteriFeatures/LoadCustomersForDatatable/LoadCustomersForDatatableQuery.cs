using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.LoadCustomersForDatatable
{
    public class LoadCustomersForDatatableQuery : DtParameters, IRequest<DtResult<LoadCustomersForDatatableResult>>
    {
        public short FirmaId { get; set; }
        public bool? AktifMi { get; set; }
    }
}
