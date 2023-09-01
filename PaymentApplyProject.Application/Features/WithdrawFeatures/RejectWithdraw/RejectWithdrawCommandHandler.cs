using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Exceptions;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Dtos.Settings;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw
{
    public class RejectWithdrawCommandHandler : IRequestHandler<RejectWithdrawCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly HttpClient _httpClient;
        private readonly ILogger<RejectWithdrawCommandHandler> _logger;
        private readonly string _token;

        public RejectWithdrawCommandHandler(IPaymentContext paymentContext, HttpClient httpClient, ILogger<RejectWithdrawCommandHandler> logger, ClientIntegrationSettings clientIntegrationSettings)
        {
            _paymentContext = paymentContext;
            _httpClient = httpClient;
            _logger = logger;
            _token = clientIntegrationSettings.Token;
        }

        public async Task<Response<NoContent>> Handle(RejectWithdrawCommand request, CancellationToken cancellationToken)
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

            withdraw.WithdrawStatusId = StatusConstants.WITHDRAW_REDDEDILDI;
            withdraw.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            var callbackBody = new WithdrawCallbackDto
            {
                CustomerId = withdraw.Customer.ExternalCustomerId,
                MethodType = withdraw.MethodType,
                Status = StatusConstants.REJECTED,
                Message = request.Message,
                TransactionId = withdraw.ExternalTransactionId,
                ExternalTransactionId = withdraw.Id,
                Amount = withdraw.Amount,
                Token = _token
            };
            var callbackResponse = await _httpClient.PostAsJsonAsync(withdraw.CallbackUrl, callbackBody, cancellationToken);
            string responseContent = await callbackResponse.Content.ReadAsStringAsync();

            _logger.LogInformation(new HttpClientLogDto
            {
                Request = callbackBody,
                Response = responseContent,
                Url = withdraw.CallbackUrl
            }.ToString());

            if (!callbackResponse.IsSuccessStatusCode)
                throw new CallbackException(responseContent, ErrorCodes.WithdrawCallbackException);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
