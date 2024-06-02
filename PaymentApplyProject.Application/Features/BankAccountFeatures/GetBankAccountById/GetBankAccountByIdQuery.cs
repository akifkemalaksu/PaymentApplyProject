using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountById
{
    public class GetBankAccountByIdQuery : IRequest<Response<GetBankAccountByIdResult>>
    {
        public int Id { get; set; }
    }
}
