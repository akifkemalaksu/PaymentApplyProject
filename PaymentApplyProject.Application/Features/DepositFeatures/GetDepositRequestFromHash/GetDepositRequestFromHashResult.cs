using PaymentApplyProject.Application.Dtos.BankDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash
{
    public class GetDepositRequestFromHashResult
    {
        public required IEnumerable<BankDto> Banks { get; set; }
        public int DepositRequestId { get; set; }
        public required string FailedUrl { get; set; }
        public int CustomerId { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal Amount { get; set; }
        public required string UniqueTransactionIdHash { get; set; }
    }
}
