using MediatR;
using PaymentApplyProject.Application.Dtos.CustomerDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw
{
    public class AddWithdrawCommand : IRequest<Response<AddWithdrawResult>>, ITransactional
    {
        public short BankId { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionId { get; set; }
        public CustomerInfoDto CustomerInfo { get; set; }
        public string MethodType { get; set; }
        public string CallbackUrl { get; set; }
        public decimal Amount { get; set; }
    }
}
