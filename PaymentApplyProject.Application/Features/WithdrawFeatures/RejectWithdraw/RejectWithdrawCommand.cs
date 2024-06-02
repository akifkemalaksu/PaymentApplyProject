using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw
{
    public class RejectWithdrawCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public int Id { get; set; }
        public string Message { get; set; }
    }
}
