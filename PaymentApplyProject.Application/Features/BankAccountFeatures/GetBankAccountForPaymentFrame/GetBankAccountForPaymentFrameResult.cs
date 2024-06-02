namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame
{
    public class GetBankAccountForPaymentFrameResult
    {
        public int BankAccountId { get; set; }
        public required string AccountNumber { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
