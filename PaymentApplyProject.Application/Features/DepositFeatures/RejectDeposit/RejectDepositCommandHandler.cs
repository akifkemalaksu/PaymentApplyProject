using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.DepositFeatures.RejectDeposit;

namespace PaymentApplyProject.Application.Features.DepositFeatures.RejectDeposit
{
    public class RejectDepositCommandHandler : IRequestHandler<RejectDepositCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public RejectDepositCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(RejectDepositCommand request, CancellationToken cancellationToken)
        {
            var deposit = await _paymentContext.Deposits.FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.Delete
            , cancellationToken);

            if (deposit == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            if (deposit.DepositStatusId == DepositStatusConstants.REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Reddedilmis);
            else if (deposit.DepositStatusId == DepositStatusConstants.ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Onaylanmis);

            deposit.DepositStatusId = DepositStatusConstants.REDDEDILDI;
            deposit.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
