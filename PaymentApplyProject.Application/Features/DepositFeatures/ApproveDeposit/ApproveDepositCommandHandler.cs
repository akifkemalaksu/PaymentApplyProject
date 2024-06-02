using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Exceptions;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services.HubServices;
using PaymentApplyProject.Domain.Constants;
using System.Net.Http.Json;

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

            var companyAuthUser = await _paymentContext.Users.FirstOrDefaultAsync(x =>
                x.UserRoles.Any(ur => ur.RoleId == RoleConstants.CUSTOMER_ID && !ur.Deleted)
                && x.UserCompanies.Any(uc => uc.CompanyId == depositRequest.CompanyId && !uc.Deleted)
                && !x.Deleted, cancellationToken);

            var callbackBody = new DepositCallbackBodyDto(
                methodType: depositRequest.MethodType,
                externalTransactionId: depositRequest.Id,
                uniqueTransactionId: depositRequest.UniqueTransactionId,
                customerId: depositRequest.CustomerId,
                amount: depositRequest.Amount,
                status: StatusConstants.APPROVED,
                message: string.Empty,
                token: _token,
                password: companyAuthUser.Password);
            var callbackResponse = await _httpClient.PostAsJsonAsync(depositRequest.CallbackUrl, callbackBody, cancellationToken);
            string responseContent = await callbackResponse.Content.ReadAsStringAsync();

            var log = new HttpClientLogDto
            {
                StatusCode = (int)callbackResponse.StatusCode,
                Request = callbackBody,
                Response = responseContent,
                Url = depositRequest.CallbackUrl
            };
            _logger.LogInformation("{@log}", log);

            if (!callbackResponse.IsSuccessStatusCode)
                throw new CallbackException(responseContent, ErrorCodes.DepositCallbackException);

            _depositPaymentHubRedirectionService.Redirect(depositRequest.SuccessUrl, depositRequest.UniqueTransactionIdHash);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
