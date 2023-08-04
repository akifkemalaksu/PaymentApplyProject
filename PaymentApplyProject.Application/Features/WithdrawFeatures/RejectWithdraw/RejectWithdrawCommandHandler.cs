using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw
{
    public class RejectWithdrawCommandHandler : IRequestHandler<RejectWithdrawCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public RejectWithdrawCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(RejectWithdrawCommand request, CancellationToken cancellationToken)
        {
            var withdraw = await _paymentContext.Withdraws.FirstOrDefaultAsync(x =>
               x.Id == request.Id
               && !x.Deleted
               , cancellationToken);

            if (withdraw == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            if (withdraw.WithdrawStatusId == StatusConstants.WITHDRAW_REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Reddedilmis);
            else if (withdraw.WithdrawStatusId == StatusConstants.WITHDRAW_ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Onaylanmis);

            withdraw.WithdrawStatusId = StatusConstants.WITHDRAW_REDDEDILDI;
            withdraw.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
