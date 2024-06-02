namespace PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest
{
    public class DepositRequestResult
    {
        public required string RedirectUrl { get; set; }
        public int ExternalTransactionId { get; set; }
    }
}
