using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.UserFeatures.LoadUsersForDatatable;

namespace PaymentApplyProject.Application.Features.UserFeatures.LoadUsersForDatatable
{
    public class LoadUsersForDatatableQuery : DtParameters, IRequest<DtResult<LoadUsersForDatatableResult>>
    {
        public bool? Active { get; set; }
    }
}
