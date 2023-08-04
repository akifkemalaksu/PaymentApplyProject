using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Exceptions;
using System.Net.Http.Json;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw
{
    public class ApproveWithdrawCommandHandler : IRequestHandler<ApproveWithdrawCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly HttpClient _httpClient;

        public ApproveWithdrawCommandHandler(IPaymentContext paymentContext, HttpClient httpClient)
        {
            _paymentContext = paymentContext;
            _httpClient = httpClient;
        }

        public async Task<Response<NoContent>> Handle(ApproveWithdrawCommand request, CancellationToken cancellationToken)
        {
            var withdraw = await _paymentContext.Withdraws.Include(x => x.Customer).FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.Deleted
                , cancellationToken);

            if (withdraw == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.VeriBulunamadi);

            if (withdraw.WithdrawStatusId == StatusConstants.WITHDRAW_REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Reddedilmis);
            else if (withdraw.WithdrawStatusId == StatusConstants.WITHDRAW_ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.Onaylanmis);

            withdraw.WithdrawStatusId = StatusConstants.WITHDRAW_ONAYLANDI;
            withdraw.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            var callbackBody = new WithdrawCallbackDto
            {
                CustomerId = withdraw.Customer.ExternalCustomerId,
                MethodType = withdraw.MethodType,
                Status = StatusConstants.APPROVED,
                TransactionId = withdraw.ExternalTransactionId
            };
            var callbackResponse = await _httpClient.PostAsJsonAsync(withdraw.CallbackUrl, callbackBody, cancellationToken);

            if (!callbackResponse.IsSuccessStatusCode)
            {
                string responseContent = await callbackResponse.Content.ReadAsStringAsync();
                throw new CallbackException(responseContent, ErrorCodes.WithdrawCallbackException);
            }

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
