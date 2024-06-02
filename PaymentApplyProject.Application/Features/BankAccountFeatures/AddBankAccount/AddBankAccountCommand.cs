using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.AddBankAccount
{
    public class AddBankAccountCommand : IRequest<Response<NoContent>>
    {
        public short BankId { get; set; }
        public required string AccountNumber { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
    }
}
