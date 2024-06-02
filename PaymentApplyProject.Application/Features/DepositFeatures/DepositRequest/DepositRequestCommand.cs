using MediatR;
using PaymentApplyProject.Application.Dtos.CustomerDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest
{
    public class DepositRequestCommand : IRequest<Response<DepositRequestResult>>, ITransactional
    {
        public required string CallbackUrl { get; set; }
        public required string SuccessUrl { get; set; }
        public required string FailedUrl { get; set; }
        public required CustomerInfoDto CustomerInfo { get; set; }
        public required string MethodType { get; set; }
        public required string UniqueTransactionId { get; set; }
        public decimal Amount { get; set; }
    }
}
