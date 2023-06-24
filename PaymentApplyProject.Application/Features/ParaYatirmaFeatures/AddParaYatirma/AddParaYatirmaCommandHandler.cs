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
                await _paymentContext.Firmalar.AddAsync(firma);
                await _paymentContext.SaveChangesAsync();
            }

            var firmaUrl = await _paymentContext.FirmaUrller.FirstOrDefaultAsync(x => x.Url == request.Url && !x.SilindiMi);
            if (firmaUrl == null)
            {
                firmaUrl = new()
                {
                    Url = request.Url,
                    FirmaId = firma.Id
                };
                await _paymentContext.Firmalar.AddAsync(firma);
            }
            else if (firmaUrl.FirmaId != firma.Id)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);

                var usingFirma = await _paymentContext.Firmalar.FirstAsync(x => x.Id == firmaUrl.FirmaId);
                return Response<NoContent>.Success(System.Net.HttpStatusCode.BadRequest, string.Format(Messages.ThisUrlUsedForCompany, usingFirma.Ad));
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
                await _paymentContext.Musteriler.AddAsync(musteri);
                await _paymentContext.SaveChangesAsync();
            }

            var paraYatirma = new ParaYatirma
            {
                MusteriId = musteri.Id,
                ParaYatirmaDurumId = ParaYatirmaDurumSabitler.BEKLIYOR,
                BankaHesapId = request.BankaHesapId,
                Tutar = request.Tutar
            };
            await _paymentContext.ParaYatirmalar.AddAsync(paraYatirma);
            await _paymentContext.SaveChangesAsync();

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
