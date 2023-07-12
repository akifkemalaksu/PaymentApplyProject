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
using PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable
{
    public class LoadCustomersForDatatableQuery : DtParameters, IRequest<DtResult<LoadCustomersForDatatableResult>>
    {
        public short CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
