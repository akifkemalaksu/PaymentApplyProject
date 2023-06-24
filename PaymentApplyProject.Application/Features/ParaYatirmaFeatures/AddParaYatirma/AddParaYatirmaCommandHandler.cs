using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma
{
    public class AddParaYatirmaCommandHandler : IRequestHandler<AddParaYatirmaCommand, Response<AddParaYatirmaResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public AddParaYatirmaCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<AddParaYatirmaResult>> Handle(AddParaYatirmaCommand request, CancellationToken cancellationToken)
        {
            var firma = await _paymentContext.Firmalar.FirstOrDefaultAsync(x => x.RequestCode == request.FirmaKodu && !x.SilindiMi);
            if (firma == null)
                return Response<AddParaYatirmaResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThisCodeNotDefined);

            var musteri = await _paymentContext.Musteriler.FirstOrDefaultAsync(x => x.KullaniciAdi == request.MusteriKullaniciAdi && x.FirmaId == firma.Id && !x.SilindiMi);
            if (musteri == null)
            {
                musteri = new Musteri
                {
                    KullaniciAdi = request.MusteriKullaniciAdi,
                    Ad = request.MusteriAd,
                    Soyad = request.MusteriSoyad,
                    FirmaId = firma.Id,
                };
                await _paymentContext.Musteriler.AddAsync(musteri);
                await _paymentContext.SaveChangesAsync();
            }

            var paraYatirma = new ParaYatirma
            {
                MusteriId = musteri.Id,
                ParaYatirmaDurumId = ParaYatirmaDurumSabitler.BEKLIYOR,
                BankaHesapId = request.BankaHesapId,
                Tutar = request.Tutar,
                EntegrasyonId = request.EntegrasyonId
            };
            await _paymentContext.ParaYatirmalar.AddAsync(paraYatirma);
            await _paymentContext.SaveChangesAsync();

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
