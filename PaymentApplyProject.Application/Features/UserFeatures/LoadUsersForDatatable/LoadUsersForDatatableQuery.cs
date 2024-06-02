using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.LoadUsersForDatatable
{
    public class LoadUsersForDatatableQuery : DtParameters, IRequest<DtResult<LoadUsersForDatatableResult>>
    {
        public bool? Active { get; set; }
    }
}
