using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Dtos.GrandPashaBetDtos;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit
{
    public class AddDepositCommandHandler : IRequestHandler<AddDepositCommand, Response<AddDepositResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IGrandPashaBetService _grandPashaBetService;
        private readonly INotificationService _notificationService;

        public AddDepositCommandHandler(IPaymentContext paymentContext, IGrandPashaBetService grandPashaBetService, INotificationService notificationService)
        {
            _paymentContext = paymentContext;
            _grandPashaBetService = grandPashaBetService;
            _notificationService = notificationService;
        }

        public async Task<Response<AddDepositResult>> Handle(AddDepositCommand request, CancellationToken cancellationToken)
        {
            Deposit deposit = new()
            {
                CustomerId = request.CustomerId,
                DepositStatusId = DepositStatusConstants.BEKLIYOR,
                BankAccountId = request.BankAccountId,
                Amount = request.Amount,
            };
            await _paymentContext.Deposits.AddAsync(deposit, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            var isExistsDeposit = await _paymentContext.Deposits.AnyAsync(x =>
                    x.CustomerId == request.CustomerId
                    && x.DepositStatusId == DepositStatusConstants.BEKLIYOR
                    && !x.Delete, cancellationToken);
            if (isExistsDeposit)
                return Response<AddDepositResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingTransaction);

            //var musteri = await _paymentContext.Musteriler.FirstOrDefaultAsync(x => x.Id == request.MusteriId);
            //CreateDepositDto createDeposit = new()
            //{
            //    KullaniciAdi = musteri.KullaniciAdi,
            //    ParaYatirmaTalepId = paraYatirma.Id,
            //    Tutar = paraYatirma.Tutar
            //};
            //var createDepositResponse = await _grandPashaBetService.CreateDepositAsync(createDeposit);

            //if (!createDepositResponse.IsSuccessful)
            //{
            //    await _paymentContext.RollbackTransactionAsync(cancellationToken);
            //    return Response<AddParaYatirmaResult>.Error(createDepositResponse.StatusCode, createDepositResponse.Message);
            //}

            //paraYatirma.EntegrasyonId = createDepositResponse.Data.EntegrasyonId;
            //await _paymentContext.SaveChangesAsync(cancellationToken);

            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

            NotificationDto notification = new()
            {
                Message = "Yeni para yatırma talebi!",
                Path = "/payment/deposits"
            };
            var usernames = await _paymentContext.Users.Where(x =>
                (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.ADMIN_ID && !ur.Delete)
                || (x.UserRoles.Any(ur => ur.RoleId == RoleConstants.USER_ID && !ur.Delete)
                    && x.UserCompanies.Any(uc => uc.CompanyId == customer.CompanyId && !uc.Delete)))
                && !x.Delete
            ).Select(x => x.Username).ToListAsync(cancellationToken);
            _notificationService.CreateNotificationToSpecificUsers(usernames, notification, cancellationToken);

            return Response<AddDepositResult>.Success(System.Net.HttpStatusCode.OK,
                new()
                {
                    DepositId = deposit.Id
                },
                Messages.OperationSuccessful
                );
        }
    }
}
