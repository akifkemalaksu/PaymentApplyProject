using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit
{
    public class AddDepositCommand : IRequest<Response<AddDepositResult>>, ITransactional
    {
        public int DepositRequestId { get; set; }
        public int CustomerId { get; set; }
        public int BankAccountId { get; set; }
    }
}
