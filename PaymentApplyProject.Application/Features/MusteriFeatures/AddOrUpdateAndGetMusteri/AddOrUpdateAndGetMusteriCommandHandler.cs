using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.AddMusteri
{
    public class AddOrUpdateAndGetMusteriCommandHandler : IRequestHandler<AddOrUpdateAndGetMusteriCommand, Response<AddOrUpdateAndGetMusteriResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public AddOrUpdateAndGetMusteriCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<AddOrUpdateAndGetMusteriResult>> Handle(AddOrUpdateAndGetMusteriCommand request, CancellationToken cancellationToken)
        {
            var firma = await _paymentContext.Firmalar.FirstOrDefaultAsync(x => x.RequestCode == request.FirmaKodu && !x.SilindiMi);
            if (firma == null)
                return Response<AddOrUpdateAndGetMusteriResult>.Error(System.Net.HttpStatusCode.NotFound, string.Format(Messages.NotFoundWithName, nameof(Firma)));

            var musteri = await _paymentContext.Musteriler.FirstOrDefaultAsync(x => x.FirmaId == firma.Id && x.KullaniciAdi == request.MusteriKullaniciAdi && !x.SilindiMi);
            if (musteri == null)
            {
                musteri = new Musteri
                {
                    KullaniciAdi = request.MusteriKullaniciAdi,
                    Ad = request.MusteriAd,
                    Soyad = request.MusteriSoyad,
                    FirmaId = firma.Id,
                    AktifMi = true
                };
                await _paymentContext.Musteriler.AddAsync(musteri);
                await _paymentContext.SaveChangesAsync();
            }
            else if (!musteri.AktifMi)
                return Response<AddOrUpdateAndGetMusteriResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.PassiveCustomer);
            else if (musteri.Ad != request.MusteriAd || musteri.Soyad != request.MusteriSoyad)
            {
                musteri.Ad = request.MusteriAd;
                musteri.Soyad = request.MusteriSoyad;
                await _paymentContext.SaveChangesAsync();
            }
            else
            {
                /*
                 * todo: burada sadece para yatırma için kontrol yapıldı 
                 * ancak bu işlem genel bir işlem olarak kullanılabilir 
                 * bu para yatırma var mı kontrolünü farklı bir feature olarak tanımlanabilir
                 * (**gereksiz işlem olmaması için yeni eklenen müşteriye kontrol yapılmaması lazım)
                 */
                var isExistsParaYatirma = await _paymentContext.ParaYatirmalar.CountAsync(x =>
                    x.MusteriId == musteri.Id
                    && x.ParaYatirmaDurumId == ParaYatirmaDurumSabitler.BEKLIYOR
                    && !x.SilindiMi) > 0;
                if (isExistsParaYatirma)
                    return Response<AddOrUpdateAndGetMusteriResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingTransaction);
            }

            var addOrUpdateAndGetMusteriResult = new AddOrUpdateAndGetMusteriResult { MusteriId = musteri.Id };
            return Response<AddOrUpdateAndGetMusteriResult>.Success(System.Net.HttpStatusCode.OK, addOrUpdateAndGetMusteriResult);
        }
    }
}
