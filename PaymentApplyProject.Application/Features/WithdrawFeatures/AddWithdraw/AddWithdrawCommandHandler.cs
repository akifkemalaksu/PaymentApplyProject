using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw
{
    public class AddWithdrawCommandHandler : IRequestHandler<AddWithdrawCommand, Response<AddWithdrawResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IJwtAuthService _jwtAuthService;

        public AddWithdrawCommandHandler(IPaymentContext paymentContext, IJwtAuthService jwtAuthService)
        {
            _paymentContext = paymentContext;
            _jwtAuthService = jwtAuthService;
        }

        public async Task<Response<AddWithdrawResult>> Handle(AddWithdrawCommand request, CancellationToken cancellationToken)
        {
            // todo: para çekme veya para yatırmada istek atan tarafın talep id sini request te almalıyız
            // todo: url i istekte mi alacağız yoksa biz mi yakalayacağız? Şimdilik istekte alıyoruz
            //var url = _httpContextAccessor.HttpContext.Request.GetTypedHeaders().Referer;

            var userInfos = _jwtAuthService.GetSignedInUserInfos();
            if (userInfos == null || !userInfos.Companies.Any())
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            var company = userInfos.Companies.First();

            var musteri = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.CompanyId == company.Id && x.Username == request.Username && !x.Delete, cancellationToken);
            if (musteri == null)
            {
                musteri = new()
                {
                    CompanyId = company.Id,
                    Username = request.Username,
                    Name = request.Name,
                    Surname = request.Surname
                };
                await _paymentContext.Customers.AddAsync(musteri, cancellationToken);
                await _paymentContext.SaveChangesAsync(cancellationToken);
            }

            var isExistsParaCekme = await _paymentContext.Withdraws.AnyAsync(x =>
                    x.CustomerId == musteri.Id
                    && x.StatusId == WithdrawStatusConstants.BEKLIYOR
                    && !x.Delete, cancellationToken);
            if (isExistsParaCekme)
                return Response<AddWithdrawResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingTransaction);

            Withdraw addParaCekme = new()
            {
                CustomerId = musteri.Id,
                Amount = request.Amount,
                AccountNumber = request.AccountNumber,
                StatusId = WithdrawStatusConstants.BEKLIYOR,
                IntegrationId = request.IntegrationId
            };

            await _paymentContext.Withdraws.AddAsync(addParaCekme, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<AddWithdrawResult>.Success(System.Net.HttpStatusCode.OK,
                new()
                {
                    WithdrawId = addParaCekme.Id
                },
                Messages.OperationSuccessful
                );
        }
    }
}
