using MediatR;
using PaymentApplyProject.Application.Dtos.BankDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankFeatures.GetBanks
{
    public class GetBanksQuery : IRequest<Response<IEnumerable<BankDto>>>
    {
    }
}
