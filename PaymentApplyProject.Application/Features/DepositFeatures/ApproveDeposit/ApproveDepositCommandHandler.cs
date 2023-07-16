using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.DepositFeatures.ApproveDeposit;

namespace PaymentApplyProject.Application.Features.DepositFeatures.ApproveDeposit
{
    public class ApproveDepositCommandHandler : IRequestHandler<ApproveDepositCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public ApproveDepositCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(ApproveDepositCommand request, CancellationToken cancellationToken)
        {
            var deposit = await _paymentContext.Deposits.FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.Delete
                , cancellationToken);

            if (deposit == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            if (deposit.DepositStatusId == DepositStatusConstants.REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyRejected);
            else if (deposit.DepositStatusId == DepositStatusConstants.ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyApproved);

            deposit.DepositStatusId = DepositStatusConstants.ONAYLANDI;
            deposit.Amount = request.Amount;
            deposit.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
