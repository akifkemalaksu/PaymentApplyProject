using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableQuery : DtParameters, IRequest<DtResult<LoadBanksForDatatableResult>>
    {
    }
}
