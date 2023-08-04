using MediatR;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.CustomerDtos;

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
