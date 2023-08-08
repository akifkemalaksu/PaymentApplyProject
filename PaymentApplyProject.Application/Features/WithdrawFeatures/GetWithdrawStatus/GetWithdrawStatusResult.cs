using PaymentApplyProject.Domain.Constants;
using System.Text.Json.Serialization;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus
{
    public class GetWithdrawStatusResult
    {
        [JsonIgnore]
        public short WithdrawStatusId { get; set; }
        public string WithdrawStatus
        {
            get => WithdrawStatusId switch
            {
                StatusConstants.WITHDRAW_BEKLIYOR => StatusConstants.PENDING,
                StatusConstants.WITHDRAW_REDDEDILDI => StatusConstants.REJECTED,
                StatusConstants.WITHDRAW_ONAYLANDI => StatusConstants.APPROVED,
            };
        }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
        public decimal Amount { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string ExternalTransactionId { get; set; }
        public string MethodType { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CustomerId { get; set; }
    }
}
