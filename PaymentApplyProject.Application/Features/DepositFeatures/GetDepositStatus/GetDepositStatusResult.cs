using PaymentApplyProject.Domain.Constants;
using System.Text.Json.Serialization;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus
{
    public class GetDepositStatusResult
    {
        [JsonIgnore]
        public short DepositStatusId { get; set; }
        public string DepositStatus => DepositStatusId switch
        {
            StatusConstants.DEPOSIT_BEKLIYOR => StatusConstants.PENDING,
            StatusConstants.DEPOSIT_REDDEDILDI => StatusConstants.REJECTED,
            StatusConstants.DEPOSIT_ONAYLANDI => StatusConstants.APPROVED,
        };
        public required string CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public required string MethodType { get; set; }
        public required string UniqueTransactionId { get; set; }
        public int ExternalTransactionId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
