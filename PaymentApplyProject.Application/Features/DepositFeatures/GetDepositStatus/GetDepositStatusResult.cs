using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus
{
    public class GetDepositStatusResult
    {
        public short DepositStatusId { get; set; }
        public string DepositStatus
        {
            get => DepositStatusId switch
            {
                0 => StatusConstants.NOT_INTEGRATED, // deposit kaydı oluşmamışsa bu değere 0 veriliyor
                StatusConstants.DEPOSIT_BEKLIYOR => StatusConstants.PENDING,
                StatusConstants.DEPOSIT_REDDEDILDI => StatusConstants.REJECTED,
                StatusConstants.DEPOSIT_ONAYLANDI => StatusConstants.APPROVED,
            };
        }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string MethodType { get; set; }
        public string UniqueTransactionId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
