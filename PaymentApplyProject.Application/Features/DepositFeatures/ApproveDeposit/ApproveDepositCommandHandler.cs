using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.DepositFeatures.ApproveDeposit;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Exceptions;
using System.Net.Http;
using System.Net.Http.Json;
using PaymentApplyProject.Application.Extensions;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Services;

namespace PaymentApplyProject.Application.Features.DepositFeatures.ApproveDeposit
{
    public class ApproveDepositCommandHandler : IRequestHandler<ApproveDepositCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IDepositPaymentHubRedirectionService _depositPaymentHubRedirectionService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApproveDepositCommandHandler> _logger;
        private readonly string _token;

        public ApproveDepositCommandHandler(IPaymentContext paymentContext, IDepositPaymentHubRedirectionService depositPaymentHubRedirectionService, HttpClient httpClient, ILogger<ApproveDepositCommandHandler> logger, ClientIntegrationSettings clientIntegrationSettings)
        {
            _paymentContext = paymentContext;
            _depositPaymentHubRedirectionService = depositPaymentHubRedirectionService;
            _httpClient = httpClient;
            _logger = logger;
            _token = clientIntegrationSettings.Token;
        }

        public async Task<Response<NoContent>> Handle(ApproveDepositCommand request, CancellationToken cancellationToken)
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

            deposit.DepositStatusId = StatusConstants.DEPOSIT_ONAYLANDI;
            deposit.TransactionDate = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.Id == deposit.DepositRequestId && !x.Deleted, cancellationToken);

            var callbackBody = new DepositCallbackBodyDto
            {
                CustomerId = depositRequest.CustomerId,
                MethodType = depositRequest.MethodType,
                Status = StatusConstants.APPROVED,
                ExternalTransactionId = depositRequest.Id,
                UniqueTransactionId = depositRequest.UniqueTransactionId,
                Amount = deposit.Amount,
                Token = _token
            };
            var callbackResponse = await _httpClient.PostAsJsonAsync(depositRequest.CallbackUrl, callbackBody, cancellationToken);
            string responseContent = await callbackResponse.Content.ReadAsStringAsync();

            _logger.LogInformation(new HttpClientLogDto
            {
                Request = callbackBody,
                Response = responseContent,
                Url = depositRequest.CallbackUrl
            }.ToString());

            if (!callbackResponse.IsSuccessStatusCode)
                throw new CallbackException(responseContent, ErrorCodes.DepositCallbackException);

            _depositPaymentHubRedirectionService.Redirect(depositRequest.SuccessUrl, depositRequest.UniqueTransactionIdHash);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
