namespace PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw
{
    public class AddWithdrawResult
    {
        public int ExternalTransactionId { get; set; }
        public required string Status { get; set; }
    }
}
