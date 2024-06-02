namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById
{
    public class GetWithdrawByIdResult
    {
        public int Id { get; set; }
        public required string ExternalTransactionId { get; set; }
        public required string Company { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public int StatusId { get; set; }
        public required string Status { get; set; }
        public required string Bank { get; set; }
        public required string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
