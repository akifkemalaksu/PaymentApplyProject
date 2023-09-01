namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash
{
    public class GetDepositRequestFromHashResult
    {
        public int DepositRequestId { get; set; }
        public string FailedUrl { get; set; }
        public int CustomerId { get; set; }
        public DateTime ValidTo { get; set; }
        public string UniqueTransactionIdHash { get; set; }
    }
}
