using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.KullaniciDtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICookieAuthService _cookieTokenService;
        private readonly ICustomMapper _customMapper;

        public LoginCommandHandler(IPaymentContext paymentContext, ICookieAuthService cookieTokenService, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _cookieTokenService = cookieTokenService;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var kullanici = await _paymentContext
                .Kullanicilar
                .Where(x =>
                    (x.Email == request.EmailKullaniciAdi || x.KullaniciAdi == request.EmailKullaniciAdi)
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
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.IncorrectLoginInfo);

            if (!kullanici.Yetkiler.Any(x => x.Id == RolSabitler.ADMIN_ID))
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.NotAuthorized);

            await _cookieTokenService.SignInAsync(kullanici, request.BeniHatirla);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK);
        }
    }
}
