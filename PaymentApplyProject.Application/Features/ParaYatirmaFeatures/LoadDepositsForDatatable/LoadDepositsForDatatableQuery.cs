using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable
{
    public class LoadDepositsForDatatableQuery : DtParameters, IRequest<DtResult<LoadDepositsForDatatableResult>>
    {
        public int BankaId { get; set; }
        public int BankaHesapId { get; set; }
        public int FirmaId { get; set; }
        public int MusteriId { get; set; }
        public int DurumId { get; set; }
    }
}
