using PaymentApplyProject.Domain.Constants;
using System.Text.Json.Serialization;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus
{
    public class GetWithdrawStatusResult
    {
        [JsonIgnore]
        public short WithdrawStatusId { get; set; }
        public string WithdrawStatus => WithdrawStatusId switch
        {
            StatusConstants.WITHDRAW_BEKLIYOR => StatusConstants.PENDING,
            StatusConstants.WITHDRAW_REDDEDILDI => StatusConstants.REJECTED,
            StatusConstants.WITHDRAW_ONAYLANDI => StatusConstants.APPROVED,
        };
        public required string AccountNumber { get; set; }
        public required string Bank { get; set; }
        public decimal Amount { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? TransactionDate { get; set; }
        public required string TransactionId { get; set; }
        public int ExternalTransactionId { get; set; }
        public required string MethodType { get; set; }
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string CustomerId { get; set; }
    }
}
