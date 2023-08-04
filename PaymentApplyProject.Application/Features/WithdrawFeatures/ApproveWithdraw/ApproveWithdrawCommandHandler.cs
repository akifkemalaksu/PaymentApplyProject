using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw
{
    public class ApproveWithdrawCommandHandler : IRequestHandler<ApproveWithdrawCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public ApproveWithdrawCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(ApproveWithdrawCommand request, CancellationToken cancellationToken)
        {
            var paraCekme = await _paymentContext.Withdraws.FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.Deleted
                , cancellationToken);

            if (paraCekme == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            if (paraCekme.WithdrawStatusId == StatusConstants.WITHDRAW_REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Reddedilmis);
            else if (paraCekme.WithdrawStatusId == StatusConstants.WITHDRAW_ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Onaylanmis);

            paraCekme.WithdrawStatusId = StatusConstants.WITHDRAW_ONAYLANDI;
            paraCekme.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
