using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForDatatable
{
    public class LoadBankAccountsForDatatableQuery : DtParameters, IRequest<DtResult<LoadBankAccountsForDatatableResult>>
    {
        public short BankId { get; set; }
        public bool? Active { get; set; }
        public decimal Amount { get; set; }
    }
}
