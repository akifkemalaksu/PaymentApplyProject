using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using System.Net.Http.Json;
using System.Web;

namespace PaymentApplyProject.Application.Features.DepositFeatures.DepositRequestsTimeoutControl
{
    public class DepositRequestsTimeoutControlCommandHandler : IRequestHandler<DepositRequestsTimeoutControlCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AddDepositCommandHandler> _logger;
        private readonly string _token;

        public DepositRequestsTimeoutControlCommandHandler(HttpClient httpClient, ILogger<AddDepositCommandHandler> logger, ClientIntegrationSettings clientIntegrationSettings, IPaymentContext paymentContext)
        {
            _httpClient = httpClient;
            _logger = logger;
            _token = clientIntegrationSettings.Token;
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(DepositRequestsTimeoutControlCommand request, CancellationToken cancellationToken)
        {
            var timeoutDepositRequests = _paymentContext.DepositRequests.Where(x => (x.Deposit == null || x.Deposit.Deleted) && x.ValidTo < DateTime.Now.AddHours(3) && !x.Deleted).ToList();

            if (!timeoutDepositRequests.Any())
                return Response<NoContent>.Success(System.Net.HttpStatusCode.OK);

            foreach (var timeoutDepositRequest in timeoutDepositRequests)
            {
                var customer = await _paymentContext.Customers.FirstOrDefaultAsync(c => c.ExternalCustomerId == timeoutDepositRequest.CustomerId && c.CompanyId == timeoutDepositRequest.CompanyId && !c.Deleted, cancellationToken);

                Deposit deposit = new()
                {
                    CustomerId = customer.Id,
                    DepositStatusId = StatusConstants.DEPOSIT_REDDEDILDI,
                    DepositRequestId = timeoutDepositRequest.Id,
                    Amount = timeoutDepositRequest.Amount,
                };
                await _paymentContext.Deposits.AddAsync(deposit, cancellationToken);
                await _paymentContext.SaveChangesAsync();

                var callbackBody = new DepositCallbackBodyDto
                {
                    CustomerId = timeoutDepositRequest.CustomerId,
                    MethodType = timeoutDepositRequest.MethodType,
                    Status = StatusConstants.REJECTED,
                    Message = Messages.DepositRequestIsTimeout,
                    ExternalTransactionId = timeoutDepositRequest.Id,
                    UniqueTransactionId = timeoutDepositRequest.UniqueTransactionId,
                    Amount = timeoutDepositRequest.Amount,
                    Token = _token
                };
                var callbackResponse = await _httpClient.PostAsJsonAsync(timeoutDepositRequest.CallbackUrl, callbackBody, cancellationToken);
                string responseContent = await callbackResponse.Content.ReadAsStringAsync();

                _logger.LogInformation(new HttpClientLogDto
                {
                    StatusCode = (int)callbackResponse.StatusCode,
                    Request = callbackBody,
                    Response = responseContent,
                    Url = timeoutDepositRequest.CallbackUrl
                }.ToString());
            }

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK);
        }
    }
}
