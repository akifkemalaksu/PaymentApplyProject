using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForSelect
{
    public class LoadBanksForSelectQuery : SelectParameters, IRequest<SelectResult>
    {

    }
}
