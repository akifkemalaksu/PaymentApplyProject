using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw;

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
                && !x.Delete
                , cancellationToken);

            if (paraCekme == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            if (paraCekme.WithdrawStatusId == WithdrawStatusConstants.REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Reddedilmis);
            else if (paraCekme.WithdrawStatusId == WithdrawStatusConstants.ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Onaylanmis);

            paraCekme.WithdrawStatusId = WithdrawStatusConstants.ONAYLANDI;
            paraCekme.ApprovedAmount = request.Amount;
            paraCekme.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
