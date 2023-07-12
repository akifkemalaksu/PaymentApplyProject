using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable
{
    public class LoadCompaniesForDatatableQuery : DtParameters, IRequest<DtResult<LoadCompaniesForDatatableResult>>
    {
        public bool? Active { get; set; }
    }
}
