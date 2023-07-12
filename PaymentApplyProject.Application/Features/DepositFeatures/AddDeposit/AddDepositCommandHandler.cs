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

namespace PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit
{
    public class AddDepositCommandHandler : IRequestHandler<AddDepositCommand, Response<AddDepositResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IGrandPashaBetService _grandPashaBetService;

        public AddDepositCommandHandler(IPaymentContext paymentContext, IGrandPashaBetService grandPashaBetService)
        {
            _paymentContext = paymentContext;
            _grandPashaBetService = grandPashaBetService;
        }

        public async Task<Response<AddDepositResult>> Handle(AddDepositCommand request, CancellationToken cancellationToken)
        {
            Deposit deposit = new()
            {
                CustomerId = request.CustomerId,
                StatusId = DepositStatusConstants.BEKLIYOR,
                BankAccountId = request.BankAccountId,
                Amount = request.Amount,
            };
            await _paymentContext.Deposits.AddAsync(deposit, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            var isExistsDeposit = await _paymentContext.Deposits.AnyAsync(x =>
                    x.CustomerId == request.CustomerId
                    && x.StatusId == DepositStatusConstants.BEKLIYOR
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
