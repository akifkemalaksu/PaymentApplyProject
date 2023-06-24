using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.GetParaCekmeById
{
    public class GetParaCekmeByIdQueryHandler : IRequestHandler<GetParaCekmeByIdQuery, Response<GetParaCekmeByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetParaCekmeByIdQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetParaCekmeByIdResult>> Handle(GetParaCekmeByIdQuery request, CancellationToken cancellationToken)
        {
            var paraCekme = await _paymentContext.ParaCekmeler.AsNoTracking().Where(x => x.Id.Equals(request.Id) && !x.SilindiMi).Select(x => new GetParaCekmeByIdResult
            {
                HesapNumarasi = x.HesapNumarasi,
                Durum = x.ParaCekmeDurum.Ad,
                Firma = x.Musteri.Firma.Ad,
                MusteriKullaniciAdi = x.Musteri.KullaniciAdi,
                MusteriAd = x.Musteri.Ad,
                MusteriSoyad = x.Musteri.Soyad,
                OnaylananTutar = x.OnaylananTutar,
                Tutar = x.Tutar
            }).FirstOrDefaultAsync();

            if (paraCekme is null)
                return Response<GetParaCekmeByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetParaCekmeByIdResult>.Success(System.Net.HttpStatusCode.OK, paraCekme);
        }
    }
}
