using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById
{
    public class GetDepositByIdResult
    {
        public int Id { get; set; }
        public string ExternalTransactionId { get; set; }
        public string Company { get; set; }
        public string CustomerUsername { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public short StatusId { get; set; }
        public string Status { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountSurname { get; set; }
        public string BankAccountNumber { get; set; }
        public string Bank { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
