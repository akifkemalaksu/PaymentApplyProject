using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable
{
    public class LoadCompaniesForDatatableQuery : DtParameters, IRequest<DtResult<LoadCompaniesForDatatableResult>>
    {
        public bool? Active { get; set; }
    }
}
