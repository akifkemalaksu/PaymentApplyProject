using PaymentApplyProject.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.CallbackDtos
{
    public class WithdrawCallbackDto
    {
        public string MethodType { get; private set; }
        public string TransactionId { get; private set; }
        public int ExternalTransactionId { get; private set; }
        public decimal Amount { get; private set; }
        public string CustomerId { get; private set; }
        public string Status { get; private set; }
        public string Message { get; private set; }
        public string Token { get; private set; }
        public string Hash { get; private set; }

        public WithdrawCallbackDto(string methodType, string transactionId, int externalTransactionId, decimal amount, string customerId, string status, string message, string token, string password)
        {
            MethodType = methodType;
            TransactionId = transactionId;
            ExternalTransactionId = externalTransactionId;
            Amount = amount;
            CustomerId = customerId;
            Status = status;
            Message = message;
            Token = token;

            Hash = GeneratorHelper.GenerateDataHashForCallback(new()
            {
                Password = password,
                Amount = Amount,
                Status = Status,
                Token = Token,
                TransactionId = TransactionId
            });
        }
    }

    public class GenerateHashDto
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
