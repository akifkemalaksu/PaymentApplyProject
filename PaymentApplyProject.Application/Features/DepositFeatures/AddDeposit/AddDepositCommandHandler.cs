using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Net.Http.Json;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.NotificationDtos;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash;
using PaymentApplyProject.Application.Exceptions;
using PaymentApplyProject.Application.Extensions;

namespace PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit
{
    public class AddDepositCommandHandler : IRequestHandler<AddDepositCommand, Response<AddDepositResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly INotificationService _notificationService;
        private readonly HttpClient _httpClient;

        public AddDepositCommandHandler(IPaymentContext paymentContext, INotificationService notificationService, HttpClient httpClient)
        {
            _paymentContext = paymentContext;
            _notificationService = notificationService;
            _httpClient = httpClient;
        }

        public async Task<Response<AddDepositResult>> Handle(AddDepositCommand request, CancellationToken cancellationToken)
        {
            var depositRequest = await _paymentContext.DepositRequests.FirstOrDefaultAsync(x => x.Id == request.DepositRequestId, cancellationToken);
            if (depositRequest == null)
                return Response<AddDepositResult>.Error(System.Net.HttpStatusCode.NotFound, string.Empty, ErrorCodes.DepositRequestIsNotFound);

            var isExistsDeposit = await _paymentContext.Deposits.AnyAsync(x => x.DepositRequestId == depositRequest.Id && !x.Deleted, cancellationToken);
            if (isExistsDeposit)
                return Response<AddDepositResult>.Error(System.Net.HttpStatusCode.NotFound, string.Empty, ErrorCodes.DepositRequestHashIsUsed);

            Deposit deposit = new()
            {
                CustomerId = request.CustomerId,
                DepositStatusId = StatusConstants.DEPOSIT_BEKLIYOR,
                BankAccountId = request.BankAccountId,
                Amount = request.Amount,
                DepositRequestId = depositRequest.Id,
            };

            await _paymentContext.Deposits.AddAsync(deposit, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);


            var callbackBody = new DepositCallbackBodyDto
            {
                CustomerId = depositRequest.CustomerId,
                Method = depositRequest.MethodType,
                Status = StatusConstants.PENDING,
                TransactionId = depositRequest.Id,
                UniqueTransactionId = depositRequest.UniqueTransactionId,
                Amount = request.Amount
            };
            var callbackResponse = await _httpClient.PostAsJsonAsync(depositRequest.CallbackUrl, callbackBody, cancellationToken);

            if (!callbackResponse.IsSuccessStatusCode)
            {
                var exceptionResponse = await callbackResponse.ExceptionResponse();
                throw new CallbackException(exceptionResponse.ToString());
            }

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

            NotificationDto notification = new()
            {
                Message = "Yeni para yatırma talebi!",
                Path = "/payment/deposits"
            };
            var usernames = await _paymentContext.Users.Where(x =>
                (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.ADMIN_ID && !ur.Deleted)
                || (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.USER_ID && !ur.Deleted)
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
