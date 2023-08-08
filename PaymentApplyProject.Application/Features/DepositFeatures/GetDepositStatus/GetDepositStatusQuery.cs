using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus
{
    public class GetDepositStatusQuery : IRequest<Response<GetDepositStatusResult>>
    {
        public string TransactionId { get; set; }
    }
    public class GetDepositStatusQueryHandler : IRequestHandler<GetDepositStatusQuery, Response<GetDepositStatusResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public GetDepositStatusQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<Response<GetDepositStatusResult>> Handle(GetDepositStatusQuery request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();

            if (userInfo == null)
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.NotAuthenticated);

            if (!userInfo.Companies.Any())
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.UserHasNoCompany);

            var companyId = userInfo.Companies.First().Id;
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId && !x.Deleted, cancellationToken);

            if (company == null)
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotFound);

            if (!company.Active)
                return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.CompanyIsNotActive);

            var deposit = await _paymentContext.Deposits.Where(x => x.DepositRequest.UniqueTransactionId == request.TransactionId && x.Customer.CompanyId == companyId && !x.Deleted).Select(x => new GetDepositStatusResult
            {
                AddDate = x.AddDate,
                Amount = x.Amount,
                CustomerId = x.Customer.ExternalCustomerId,
                MethodType = x.DepositRequest.MethodType,
                Name = x.Customer.Name,
                Surname = x.Customer.Surname,
                TransactionDate = x.TransactionDate,
                UniqueTransactionId = request.TransactionId,
                Username = x.Customer.Username
            }).FirstOrDefaultAsync(cancellationToken);

            if (deposit == null)
            {
                var depositRequest = await _paymentContext.DepositRequests.Where(x => x.UniqueTransactionId == request.TransactionId && x.CompanyId == companyId && !x.Deleted).Select(x => new GetDepositStatusResult
                {
                    AddDate = x.AddDate,
                    CustomerId = x.CustomerId,
                    MethodType = x.MethodType,
                    Name = x.Name,
                    Surname = x.Surname,
                    UniqueTransactionId = x.UniqueTransactionId,
                    Username = x.Username
                }).FirstOrDefaultAsync(cancellationToken);

                if (depositRequest == null)
                    return Response<GetDepositStatusResult>.Error(System.Net.HttpStatusCode.BadRequest, string.Empty, ErrorCodes.DepositIsNotFound);

                return Response<GetDepositStatusResult>.Success(System.Net.HttpStatusCode.OK, depositRequest);
            }

            return Response<GetDepositStatusResult>.Success(System.Net.HttpStatusCode.OK, deposit);
        }
    }
    public class GetDepositStatusResult
    {
        public short DepositStatusId { get; set; }
        public string DepositStatus
        {
            get => DepositStatusId switch
            {
                0 => StatusConstants.NOT_INTEGRATED, // deposit kaydı oluşmamışsa bu değere 0 veriliyor
                StatusConstants.DEPOSIT_BEKLIYOR => StatusConstants.PENDING,
                StatusConstants.DEPOSIT_REDDEDILDI => StatusConstants.REJECTED,
                StatusConstants.DEPOSIT_ONAYLANDI => StatusConstants.APPROVED,
            };
        }
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string MethodType { get; set; }
        public string UniqueTransactionId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime AddDate { get; set; }
    }
}
