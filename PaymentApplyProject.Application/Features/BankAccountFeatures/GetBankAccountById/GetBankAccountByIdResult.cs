namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountById
{
    public class GetBankAccountByIdResult
    {
        public int Id { get; set; }
        public short BankId { get; set; }
        public required string Bank { get; set; }
        public required string AccountNumber { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public bool Active { get; set; }
    }
}
