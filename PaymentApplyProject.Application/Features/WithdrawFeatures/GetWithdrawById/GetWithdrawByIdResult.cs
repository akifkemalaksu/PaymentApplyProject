namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById
{
    public class GetWithdrawByIdResult
    {
        public int Id { get; set; }
        public string ExternalTransactionId { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
