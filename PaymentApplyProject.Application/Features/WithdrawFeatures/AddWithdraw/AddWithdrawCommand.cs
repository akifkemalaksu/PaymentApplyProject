using MediatR;
using PaymentApplyProject.Application.Dtos.CustomerDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw
{
    public class AddWithdrawCommand : IRequest<Response<AddWithdrawResult>>, ITransactional
    {
        public short BankId { get; set; }
        public required string AccountNumber { get; set; }
        public required string TransactionId { get; set; }
        public required CustomerInfoDto CustomerInfo { get; set; }
        public required string MethodType { get; set; }
        public required string CallbackUrl { get; set; }
        public decimal Amount { get; set; }
    }
}
