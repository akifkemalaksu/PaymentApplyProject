namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountById
{
    public class GetBankAccountByIdResult
    {
        public int Id { get; set; }
        public short BankId { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public bool Active { get; set; }
    }
}
