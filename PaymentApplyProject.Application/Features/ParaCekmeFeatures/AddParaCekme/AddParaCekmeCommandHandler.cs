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

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme
{
    public class AddParaCekmeCommandHandler : IRequestHandler<AddParaCekmeCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddParaCekmeCommandHandler(IPaymentContext paymentContext, IHttpContextAccessor httpContextAccessor)
        {
            _paymentContext = paymentContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<NoContent>> Handle(AddParaCekmeCommand request, CancellationToken cancellationToken)
        {
            // todo: url i istekte mi alacağız yoksa biz mi yakalayacağız? Şimdilik istekte alıyoruz
            //var url = _httpContextAccessor.HttpContext.Request.GetTypedHeaders().Referer;

            var firma = await _paymentContext.Firmalar.FirstOrDefaultAsync(x => x.Ad == request.FirmaAdi && !x.SilindiMi);
            if (firma == null)
            {
                firma = new()
                {
                    Ad = request.FirmaAdi,
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

            var musteri = await _paymentContext.Musteriler.FirstOrDefaultAsync(x => x.FirmaId == firma.Id && x.KullaniciAdi == request.MusteriKullaniciAdi && !x.SilindiMi);
            if (musteri == null)
            {
                musteri = new()
                {
                    FirmaId = firma.Id,
                    KullaniciAdi = request.MusteriKullaniciAdi,
                    Ad = request.MusteriAd,
                    Soyad = request.MusteriSoyad
                };
                await _paymentContext.Musteriler.AddAsync(musteri);
                await _paymentContext.SaveChangesAsync();
            }

            var addParaCekme = new ParaCekme
            {
                MusteriId = musteri.Id,
                Tutar = request.Tutar,
                HesapNumarasi = request.HesapNumarasi,
                ParaCekmeDurumId = ParaCekmeDurumSabitler.BEKLIYOR
            };

            await _paymentContext.ParaCekmeler.AddAsync(addParaCekme);
            await _paymentContext.SaveChangesAsync();

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
