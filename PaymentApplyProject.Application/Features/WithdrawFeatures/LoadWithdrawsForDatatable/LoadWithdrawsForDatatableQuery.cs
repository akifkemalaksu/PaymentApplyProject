using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.LoadWithdrawsForDatatable
{
    public class LoadWithdrawsForDatatableQuery : DtParameters, IRequest<DtResult<LoadWithdrawsForDatatableResult>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CompanyId { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
    }
}
