using MediatR;
using PaymentApplyProject.Application.Dtos.CustomerDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest
{
    public class DepositRequestCommand : IRequest<Response<DepositRequestResult>>, ITransactional
    {
        public string CallbackUrl { get; set; }
        public string SuccessUrl { get; set; }
        public string FailedUrl { get; set; }
        public CustomerInfoDto CustomerInfo { get; set; }
        public string MethodType { get; set; }
        public string UniqueTransactionId { get; set; }
        public decimal Amount { get; set; }
    }
}
