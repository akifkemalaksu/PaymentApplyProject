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
    public class AddParaCekmeCommandHandler : IRequestHandler<AddParaCekmeCommand, Response<AddParaCekmeResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public AddParaCekmeCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<AddParaCekmeResult>> Handle(AddParaCekmeCommand request, CancellationToken cancellationToken)
        {
            // todo: para çekme veya para yatırmada istek atan tarafın talep id sini request te almalıyız
            // todo: url i istekte mi alacağız yoksa biz mi yakalayacağız? Şimdilik istekte alıyoruz
            //var url = _httpContextAccessor.HttpContext.Request.GetTypedHeaders().Referer;

            var firma = await _paymentContext.Firmalar.FirstOrDefaultAsync(x => x.RequestCode == request.FirmaKodu && !x.SilindiMi, cancellationToken);
            if (firma == null)
                return Response<AddParaCekmeResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThisCodeNotDefined);

            var musteri = await _paymentContext.Musteriler.FirstOrDefaultAsync(x => x.FirmaId == firma.Id && x.KullaniciAdi == request.MusteriKullaniciAdi && !x.SilindiMi, cancellationToken);
            if (musteri == null)
            {
                musteri = new()
                {
                    FirmaId = firma.Id,
                    KullaniciAdi = request.MusteriKullaniciAdi,
                    Ad = request.MusteriAd,
                    Soyad = request.MusteriSoyad
                };
                await _paymentContext.Musteriler.AddAsync(musteri, cancellationToken);
                await _paymentContext.SaveChangesAsync(cancellationToken);
            }

            var isExistsParaCekme = await _paymentContext.ParaCekmeler.AnyAsync(x =>
                    x.MusteriId == musteri.Id
                    && x.ParaCekmeDurumId == ParaCekmeDurumSabitler.BEKLIYOR
                    && !x.SilindiMi, cancellationToken);
            if (isExistsParaCekme)
                return Response<AddParaCekmeResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPendingTransaction);

            ParaCekme addParaCekme = new()
            {
                MusteriId = musteri.Id,
                Tutar = request.Tutar,
                HesapNumarasi = request.HesapNumarasi,
                ParaCekmeDurumId = ParaCekmeDurumSabitler.BEKLIYOR,
                EntegrasyonId = request.EntegrasyonId
            };

            await _paymentContext.ParaCekmeler.AddAsync(addParaCekme, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<AddParaCekmeResult>.Success(System.Net.HttpStatusCode.OK,
                new()
                {
                    ParaCekmeTalepId = addParaCekme.Id
                },
                Messages.OperationSuccessful
                );
        }
    }
}
