using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankaHesabi
{
    public class GetBankaHesabiQueryHandler : IRequestHandler<GetBankaHesabiQuery, Response<GetBankaHesabiResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetBankaHesabiQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetBankaHesabiResult>> Handle(GetBankaHesabiQuery request, CancellationToken cancellationToken)
        {
            var bankaHesabi = await _paymentContext.BankaHesaplari
                .AsNoTracking()
                .Where(x =>
                    x.BankaId == request.BankaId
                    && x.AltLimit <= request.Tutar
                    && x.UstLimit >= request.Tutar
                    && x.AktifMi
                    && !x.SilindiMi)
                .Select(x =>
                new GetBankaHesabiResult
                {
                    BankaHesapId = x.Id,
                    Ad = x.Ad,
                    Soyad = x.Soyad,
                    HesapNumarasi = x.HesapNumarasi,
                }).FirstOrDefaultAsync();

            if (bankaHesabi == null)
                return Response<GetBankaHesabiResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetBankaHesabiResult>.Success(System.Net.HttpStatusCode.OK, bankaHesabi);
        }
    }
}
