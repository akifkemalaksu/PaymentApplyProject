using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Core.Dtos;

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
            var paraCekme = await _paymentContext.ParaCekmeler.AsNoTracking().Where(x => x.Id.Equals(request.Id)).Select(x => new GetParaCekmeByIdResult
            {
                BankaHesapIban = x.BankaHesapIban,
                Durum = x.Durum.Ad,
                Firma = x.Firma.Ad,
                MusteriKullaniciAdi = x.Musteri.KullaniciAdi,
                OnaylananTutar = x.OnaylananTutar,
                Tutar = x.Tutar
            }).FirstOrDefaultAsync();

            if (paraCekme is null)
                return Response<GetParaCekmeByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetParaCekmeByIdResult>.Success(System.Net.HttpStatusCode.OK, paraCekme);
        }
    }
}
