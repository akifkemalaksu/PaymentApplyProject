using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma
{
    public class AddParaYatirmaCommandHandler : IRequestHandler<AddParaYatirmaCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public AddParaYatirmaCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(AddParaYatirmaCommand request, CancellationToken cancellationToken)
        {
            var firma = await _paymentContext.Firmalar.FirstOrDefaultAsync(x => x.Ad == request.FirmaAdi && !x.SilindiMi);
            if (firma == null)
            {
                firma = new Firma
                {
                    Ad = request.FirmaAdi
                };
                _paymentContext.Firmalar.Add(firma);
                await _paymentContext.SaveChangesAsync();
            }

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
                _paymentContext.Musteriler.Add(musteri);
                await _paymentContext.SaveChangesAsync();
            }

            var paraYatirma = new ParaYatirma
            {
                MusteriId = musteri.Id,
                ParaYatirmaDurumId = ParaYatirmaDurumSabitler.BEKLIYOR,
                BankaHesapId = request.BankaHesapId,
                Tutar = request.Tutar
            };
            _paymentContext.ParaYatirmalar.Add(paraYatirma);

            await _paymentContext.SaveChangesAsync();

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
