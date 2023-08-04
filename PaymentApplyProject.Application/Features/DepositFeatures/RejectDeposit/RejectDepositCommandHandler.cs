using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.DepositFeatures.RejectDeposit;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Exceptions;
using System.Net.Http;
using System.Net.Http.Json;
using PaymentApplyProject.Application.Extensions;

namespace PaymentApplyProject.Application.Features.DepositFeatures.RejectDeposit
{
    public class RejectDepositCommandHandler : IRequestHandler<RejectDepositCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly HttpClient _httpClient;

        public RejectDepositCommandHandler(IPaymentContext paymentContext, HttpClient httpClient)
        {
            _paymentContext = paymentContext;
            _httpClient = httpClient;
        }

        public async Task<Response<NoContent>> Handle(RejectDepositCommand request, CancellationToken cancellationToken)
        {
            var deposit = await _paymentContext.Deposits.FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.Deleted
            , cancellationToken);

            if (deposit == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            if (deposit.DepositStatusId == StatusConstants.DEPOSIT_REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Reddedilmis);
            else if (deposit.DepositStatusId == StatusConstants.DEPOSIT_ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Onaylanmis);

            deposit.DepositStatusId = StatusConstants.DEPOSIT_REDDEDILDI;
            deposit.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.Id == deposit.DepositRequestId && !x.Deleted, cancellationToken);

            var callbackBody = new DepositCallbackBodyDto
            {
                CustomerId = depositRequest.CustomerId,
                Method = depositRequest.MethodType,
                Status = StatusConstants.REJECTED,
                TransactionId = depositRequest.Id,
                UniqueTransactionId = depositRequest.UniqueTransactionId,
            };
            var callbackResponse = await _httpClient.PostAsJsonAsync(depositRequest.CallbackUrl, callbackBody, cancellationToken);

            if (!callbackResponse.IsSuccessStatusCode)
            {
                var exceptionResponse = await callbackResponse.ExceptionResponse();
                throw new CallbackException(exceptionResponse.ToString());
            }

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
