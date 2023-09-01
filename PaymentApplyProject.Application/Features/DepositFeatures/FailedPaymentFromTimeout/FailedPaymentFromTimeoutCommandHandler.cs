using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Exceptions;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using System.Net.Http.Json;

namespace PaymentApplyProject.Application.Features.DepositFeatures.FailedPaymentFromTimeout
{
    public class FailedPaymentFromTimeoutCommandHandler : IRequestHandler<FailedPaymentFromTimeoutCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AddDepositCommandHandler> _logger;
        private readonly string _token;

        public FailedPaymentFromTimeoutCommandHandler(HttpClient httpClient, ILogger<AddDepositCommandHandler> logger, ClientIntegrationSettings clientIntegrationSettings, IPaymentContext paymentContext)
        {
            _httpClient = httpClient;
            _logger = logger;
            _token = clientIntegrationSettings.Token;
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(FailedPaymentFromTimeoutCommand request, CancellationToken cancellationToken)
        {
            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (depositRequest == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.DepositRequestIsNotFound, ErrorCodes.DepositRequestIsNotFound);

            var isExistsDeposit = await _paymentContext.Deposits.AnyAsync(x => x.DepositRequestId == depositRequest.Id && !x.Deleted, cancellationToken);
            if (isExistsDeposit)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.DepositRequestHashIsUsed, ErrorCodes.DepositRequestHashIsUsed);

            var callbackBody = new DepositCallbackBodyDto
            {
                CustomerId = depositRequest.CustomerId,
                MethodType = depositRequest.MethodType,
                Status = StatusConstants.REJECTED,
                Message = Messages.DepositRequestIsTimeout,
                ExternalTransactionId = depositRequest.Id,
                UniqueTransactionId = depositRequest.UniqueTransactionId,
                Amount = default,
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

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK);
        }
    }
}
