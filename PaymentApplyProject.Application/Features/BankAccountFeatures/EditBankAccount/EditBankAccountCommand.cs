using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.EditBankAccount
{
    public class EditBankAccountCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public short BankId { get; set; }
        public required string AccountNumber { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public bool Active { get; set; }
    }
}
