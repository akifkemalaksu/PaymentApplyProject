using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.Http.Json;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.NotificationDtos;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash;
using PaymentApplyProject.Application.Exceptions;
using PaymentApplyProject.Application.Extensions;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Dtos.LogDtos;
using System.Text.Json;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Services.HubServices;

namespace PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit
{
    public class AddDepositCommandHandler : IRequestHandler<AddDepositCommand, Response<AddDepositResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly INotificationService _notificationService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AddDepositCommandHandler> _logger;
        private readonly string _token;

        public AddDepositCommandHandler(IPaymentContext paymentContext, INotificationService notificationService, HttpClient httpClient, ILogger<AddDepositCommandHandler> logger, ClientIntegrationSettings clientIntegrationSettings)
        {
            _paymentContext = paymentContext;
            _notificationService = notificationService;
            _httpClient = httpClient;
            _logger = logger;
            _token = clientIntegrationSettings.Token;
        }

        public async Task<Response<AddDepositResult>> Handle(AddDepositCommand request, CancellationToken cancellationToken)
        {
            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.Id == request.DepositRequestId, cancellationToken);
            if (depositRequest == null)
                return Response<AddDepositResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.DepositRequestIsNotFound, ErrorCodes.DepositRequestIsNotFound);

            var isExistsDeposit = await _paymentContext.Deposits.AnyAsync(x => x.DepositRequestId == depositRequest.Id && !x.Deleted, cancellationToken);
            if (isExistsDeposit)
                return Response<AddDepositResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.DepositRequestHashIsUsed, ErrorCodes.DepositRequestHashIsUsed);

            Deposit deposit = new()
            {
                CustomerId = request.CustomerId,
                DepositStatusId = StatusConstants.DEPOSIT_BEKLIYOR,
                BankAccountId = request.BankAccountId,
                Amount = depositRequest.Amount,
                DepositRequestId = depositRequest.Id,
            };

            await _paymentContext.Deposits.AddAsync(deposit, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

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
                status: StatusConstants.PENDING,
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

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

            NotificationDto notification = new()
            {
                Message = "Yeni para yatırma talebi!",
                Path = "/payment/deposits"
            };
            var usernames = await _paymentContext.Users.Where(x =>
                (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.ADMIN_ID && !ur.Deleted)
                || (x.UserRoles.Any(ur => new short[] { RoleConstants.USER_ID, RoleConstants.ACCOUNTING_ID }.Contains(ur.RoleId) && !ur.Deleted)
                    && x.UserCompanies.Any(uc => uc.CompanyId == customer.CompanyId && !uc.Deleted)))
                && !x.Deleted
            ).Select(x => x.Username).ToListAsync(cancellationToken);
            _notificationService.CreateNotificationToSpecificUsers(usernames, notification, cancellationToken);

            return Response<AddDepositResult>.Success(System.Net.HttpStatusCode.OK,
                new()
                {
                    DepositId = deposit.Id
                },
                Messages.IslemBasarili
                );
        }
    }
}
