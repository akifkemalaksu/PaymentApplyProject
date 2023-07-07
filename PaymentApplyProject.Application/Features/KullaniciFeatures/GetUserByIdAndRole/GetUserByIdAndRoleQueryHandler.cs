using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.GetUserByIdAndRole
{
    public class GetUserByIdAndRoleQueryHandler : IRequestHandler<GetUserByIdAndRoleQuery, Response<GetUserByIdAndRoleResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public GetUserByIdAndRoleQueryHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<GetUserByIdAndRoleResult>> Handle(GetUserByIdAndRoleQuery request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Kullanicilar
                .Where(x =>
                x.Id == request.Id
                && x.KullaniciYetkiler.Any(ky =>
                    ky.YetkiId == request.YetkiId
                    && !ky.SilindiMi
                )
                && !x.SilindiMi)
                .Select(x => new GetUserByIdAndRoleResult
                {
                    Id = request.Id,
                    Ad = x.Ad,
                    Soyad = x.Soyad,
                    Email = x.Email,
                    KullaniciAdi = x.KullaniciAdi,
                    AktifMi = x.AktifMi,
                    Firmalar = x.KullaniciFirmalar.Where(x => !x.SilindiMi).Select(kf => new FirmaDto
                    {
                        Ad = kf.Firma.Ad,
                        Id = kf.FirmaId
                    })
                    .ToList(),
                    Yetkiler = x.KullaniciYetkiler.Where(x => !x.SilindiMi).Select(ky => new YetkiDto
                    {
                        Ad = ky.Yetki.Ad,
                        Id = ky.YetkiId
                    })
                    .ToList(),
                }).FirstOrDefaultAsync();

            if (user is null)
                return Response<GetUserByIdAndRoleResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetUserByIdAndRoleResult>.Success(System.Net.HttpStatusCode.OK, user);
        }
    }
}
