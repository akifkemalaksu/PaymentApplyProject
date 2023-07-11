using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Features.BankaFeatures.LoadBanksForDatatable;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.FirmaFeatures.LoadCompaniesForDatatable
{
    public class LoadCompaniesForDatatableQuery : DtParameters, IRequest<DtResult<LoadCompaniesForDatatableResult>>
    {
        public bool? AktifMi { get; set; }
    }
}
