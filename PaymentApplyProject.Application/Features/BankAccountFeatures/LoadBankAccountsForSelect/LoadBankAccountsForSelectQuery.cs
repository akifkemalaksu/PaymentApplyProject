using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForSelect
{
    public class LoadBankAccountsForSelectQuery : SelectParameters, IRequest<SelectResult>
    {
        public int BankId { get; set; }
    }
}
