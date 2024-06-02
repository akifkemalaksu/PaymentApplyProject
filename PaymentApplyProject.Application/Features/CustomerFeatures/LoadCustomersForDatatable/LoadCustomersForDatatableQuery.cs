using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable
{
    public class LoadCustomersForDatatableQuery : DtParameters, IRequest<DtResult<LoadCustomersForDatatableResult>>
    {
        public short CompanyId { get; set; }
        public bool? Active { get; set; }
    }
}
