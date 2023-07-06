using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankaHesabiForPaymentFrame;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankaHesabiForPaymentFrame
{
    public class GetBankaHesabiForPaymentFrameQueryHandler : IRequestHandler<GetBankaHesabiForPaymentFrameQuery, Response<GetBankaHesabiForPaymentFrameResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public GetBankaHesabiForPaymentFrameQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<GetBankaHesabiForPaymentFrameResult>> Handle(GetBankaHesabiForPaymentFrameQuery request, CancellationToken cancellationToken)
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
                new GetBankaHesabiForPaymentFrameResult
                {
                    BankaHesapId = x.Id,
                    Ad = x.Ad,
                    Soyad = x.Soyad,
                    HesapNumarasi = x.HesapNumarasi,
                }).FirstOrDefaultAsync(cancellationToken);

            if (bankaHesabi == null)
                return Response<GetBankaHesabiForPaymentFrameResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetBankaHesabiForPaymentFrameResult>.Success(System.Net.HttpStatusCode.OK, bankaHesabi);
        }
    }
}
