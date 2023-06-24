using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.GetParaYatirmaById
{
    public class GetParaYatirmaByIdQueryHandler : IRequestHandler<GetParaYatirmaByIdQuery, Response<GetParaYatirmaByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetParaYatirmaByIdQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetParaYatirmaByIdResult>> Handle(GetParaYatirmaByIdQuery request, CancellationToken cancellationToken)
        {
            var paraYatirma = await _paymentContext.ParaYatirmalar.AsNoTracking().Where(x => x.Id == request.Id && !x.SilindiMi).Select(x => new GetParaYatirmaByIdResult
            {
                Banka = x.BankaHesabi.Banka.Ad,
                Durum = x.ParaYatirmaDurum.Ad,
                Firma = x.Musteri.Firma.Ad,
                HesapNumarasi = x.BankaHesabi.HesapNumarasi,
                MusteriKullaniciAdi = x.Musteri.KullaniciAdi,
                MusteriAd = x.Musteri.Ad,
                MusteriSoyad = x.Musteri.Soyad,
                OnaylananTutar = x.OnaylananTutar,
                Tutar = x.Tutar
            }).FirstOrDefaultAsync();

            if (paraYatirma == null)
                return Response<GetParaYatirmaByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetParaYatirmaByIdResult>.Success(System.Net.HttpStatusCode.OK, paraYatirma);
        }
    }
}
