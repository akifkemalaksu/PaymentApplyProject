using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken
{
    public class AuthenticateTokenCommandHandler : IRequestHandler<AuthenticateTokenCommand, Response<AuthenticateTokenResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IJwtAuthService _jwtTokenService;
        private readonly ICustomMapper _customMapper;

        public AuthenticateTokenCommandHandler(IPaymentContext paymentContext, IJwtAuthService jwtTokenService, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _jwtTokenService = jwtTokenService;
            _customMapper = customMapper;
        }

        public async Task<Response<AuthenticateTokenResult>> Handle(AuthenticateTokenCommand request, CancellationToken cancellationToken)
        {
            var kullanici = await _paymentContext.Kullanicilar
                .Where(x =>
                    x.KullaniciAdi == request.KullaniciAdi
                    && x.Sifre == request.Sifre
                    && !x.SilindiMi)
                .Select(x => new KullaniciDto
                {
                    Ad = x.Ad,
                    Email = x.Email,
                    KullaniciAdi = x.KullaniciAdi,
                    Soyad = x.Soyad,
                    Id = x.Id,
                    Firmalar = x.KullaniciFirmalar.Select(kf => new FirmaDto
                    {
                        Ad = kf.Firma.Ad,
                        Id = kf.FirmaId
                    }),
                    Yetkiler = x.KullaniciYetkiler.Select(ky => new YetkiDto
                    {
                        Ad = ky.Yetki.Ad,
                        Id = ky.YetkiId
                    })
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (kullanici == null)
                return Response<AuthenticateTokenResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            if (!kullanici.Yetkiler.Any(x => x.Id == RolSabitler.CUSTOMER_ID))
                return Response<AuthenticateTokenResult>.Error(System.Net.HttpStatusCode.BadRequest, Messages.NotAuthorized);

            return _jwtTokenService.CreateToken(kullanici);
        }
    }
}
