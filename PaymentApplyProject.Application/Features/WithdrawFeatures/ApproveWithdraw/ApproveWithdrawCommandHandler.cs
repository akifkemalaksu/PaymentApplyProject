using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Dtos.CallbackDtos;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Exceptions;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using System.Net.Http.Json;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw
{
    public class ApproveWithdrawCommandHandler : IRequestHandler<ApproveWithdrawCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApproveWithdrawCommandHandler> _logger;
        private readonly string _token;

        public ApproveWithdrawCommandHandler(IPaymentContext paymentContext, HttpClient httpClient, ILogger<ApproveWithdrawCommandHandler> logger, ClientIntegrationSettings clientIntegrationSettings)
        {
            _paymentContext = paymentContext;
            _httpClient = httpClient;
            _logger = logger;
            _token = clientIntegrationSettings.Token;
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

            var companyAuthUser = await _paymentContext.Users.FirstOrDefaultAsync(x =>
               x.UserRoles.Any(ur => ur.RoleId == RoleConstants.CUSTOMER_ID && !ur.Deleted)
               && x.UserCompanies.Any(uc => uc.CompanyId == withdraw.Customer.CompanyId && !uc.Deleted)
               && !x.Deleted, cancellationToken);

            var callbackBody = new WithdrawCallbackDto(
                methodType: withdraw.MethodType,
                transactionId: withdraw.ExternalTransactionId,
                externalTransactionId: withdraw.Id,
                amount: withdraw.Amount,
                customerId: withdraw.Customer.ExternalCustomerId,
                status: StatusConstants.APPROVED,
                message: string.Empty,
                token: _token,
                password: companyAuthUser.Password);

            var callbackResponse = await _httpClient.PostAsJsonAsync(withdraw.CallbackUrl, callbackBody, cancellationToken);
            string responseContent = await callbackResponse.Content.ReadAsStringAsync();

            var log = new HttpClientLogDto
            {
                StatusCode = (int)callbackResponse.StatusCode,
                Request = callbackBody,
                Response = responseContent,
                Url = withdraw.CallbackUrl
            };
            _logger.LogInformation("{@log}", log);

            if (!callbackResponse.IsSuccessStatusCode)
                throw new CallbackException(responseContent, ErrorCodes.WithdrawCallbackException);


            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
