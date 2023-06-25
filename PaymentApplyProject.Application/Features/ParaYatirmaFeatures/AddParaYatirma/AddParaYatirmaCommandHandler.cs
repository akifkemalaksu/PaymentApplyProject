using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Application.Dtos.GrandPashaBetDtos;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma
{
    public class AddParaYatirmaCommandHandler : IRequestHandler<AddParaYatirmaCommand, Response<AddParaYatirmaResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IGrandPashaBetService _grandPashaBetService;

        public AddParaYatirmaCommandHandler(IPaymentContext paymentContext, IGrandPashaBetService grandPashaBetService)
        {
            _paymentContext = paymentContext;
            _grandPashaBetService = grandPashaBetService;
        }

        public async Task<Response<AddParaYatirmaResult>> Handle(AddParaYatirmaCommand request, CancellationToken cancellationToken)
        {
            var paraYatirma = new ParaYatirma
            {
                MusteriId = request.MusteriId,
                ParaYatirmaDurumId = ParaYatirmaDurumSabitler.BEKLIYOR,
                BankaHesabiId = request.BankaHesapId,
                Tutar = request.Tutar,
            };
            await _paymentContext.ParaYatirmalar.AddAsync(paraYatirma);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            var musteri = await _paymentContext.Musteriler.FirstOrDefaultAsync(x => x.Id == request.MusteriId);
            CreateDepositDto createDeposit = new()
            {
                KullaniciAdi = musteri.KullaniciAdi,
                ParaYatirmaTalepId = paraYatirma.Id
            };
            var createDepositResponse = await _grandPashaBetService.CreateDepositAsync(createDeposit);

            if (!createDepositResponse.IsSuccessful)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);
                return Response<AddParaYatirmaResult>.Error(createDepositResponse.StatusCode, createDepositResponse.Message);
            }

            paraYatirma.EntegrasyonId = createDepositResponse.Data.EntegrasyonId;
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<AddParaYatirmaResult>.Success(System.Net.HttpStatusCode.OK,
                new()
                {
                    ParaYatirmaTalepId = paraYatirma.Id
                },
                Messages.OperationSuccessful
                );
        }
    }
}
