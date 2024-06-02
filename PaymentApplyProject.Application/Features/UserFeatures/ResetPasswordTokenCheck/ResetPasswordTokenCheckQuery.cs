using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.ResetPasswordTokenCheck
{
    public class ResetPasswordTokenCheckQuery : IRequest<Response<ResetPasswordTokenCheckResult>>
    {
        public string Token { get; set; }
    }
}
