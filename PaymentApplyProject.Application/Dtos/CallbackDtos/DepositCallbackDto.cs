using PaymentApplyProject.Application.Helpers;

namespace PaymentApplyProject.Application.Dtos.CallbackDtos
{
    public class DepositCallbackBodyDto
    {
        public string MethodType { get; private set; }
        public int ExternalTransactionId { get; private set; }
        public string UniqueTransactionId { get; private set; }
        public string CustomerId { get; private set; }
        public decimal Amount { get; private set; }
        public string Status { get; private set; }
        public string Message { get; private set; }
        public string Token { get; private set; }
        public string Hash { get; private set; }

        public DepositCallbackBodyDto(string methodType, int externalTransactionId, string uniqueTransactionId, string customerId, decimal amount, string status, string message, string token, string password)
        {
            MethodType = methodType;
            ExternalTransactionId = externalTransactionId;
            UniqueTransactionId = uniqueTransactionId;
            CustomerId = customerId;
            Amount = amount;
            Status = status;
            Message = message;
            Token = token;

            Hash = GeneratorHelper.GenerateDataHashForCallback(new()
            {
                Password = password,
                Amount = Amount,
                Status = Status,
                Token = Token,
                TransactionId = uniqueTransactionId
            });
        }
    }
}
