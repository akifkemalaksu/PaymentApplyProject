using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.DepositRequestsTimeoutControl
{
    public class DepositRequestsTimeoutControlCommand : IRequest<Response<NoContent>>
    {
    }
}
