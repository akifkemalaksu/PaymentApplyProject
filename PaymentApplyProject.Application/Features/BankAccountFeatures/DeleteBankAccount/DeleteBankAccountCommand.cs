using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.DeleteBankAccount
{
    public class DeleteBankAccountCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
    }
}
