namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById
{
    public class GetDepositByIdResult
    {
        public int Id { get; set; }
        public required string ExternalTransactionId { get; set; }
        public required string Company { get; set; }
        public required string CustomerUsername { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerSurname { get; set; }
        public short StatusId { get; set; }
        public required string Status { get; set; }
        public required string BankAccountName { get; set; }
        public required string BankAccountSurname { get; set; }
        public required string BankAccountNumber { get; set; }
        public required string Bank { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
