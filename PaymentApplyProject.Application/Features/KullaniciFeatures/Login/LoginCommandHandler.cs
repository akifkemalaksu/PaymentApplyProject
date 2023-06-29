using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
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
            var kullanici = await _paymentContext.Kullanicilar.FirstOrDefaultAsync(x =>
                    (x.Email == request.EmailKullaniciAdi || x.KullaniciAdi == request.EmailKullaniciAdi)
                    && x.Sifre == request.Sifre
                    && !x.SilindiMi);
            if (kullanici == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.IncorrectLoginInfo);

            var yetkiler = await _paymentContext.Yetkiler.Where(x =>
                x.KullaniciYetkiler.Any(ky =>
                    ky.KullaniciId == kullanici.Id
                    && !ky.SilindiMi)
                && !x.SilindiMi).Select(x => x.Ad).ToArrayAsync();

            if (!yetkiler.Any(x => x == RolSabitler.ADMIN))
                return Response<NoContent>.Error(System.Net.HttpStatusCode.OK, Messages.NotAuthorized);

            var kullaniciDto = _customMapper.Map<KullaniciDto>(kullanici);
            kullaniciDto.Yetkiler = yetkiler;

            await _cookieTokenService.SignInAsync(kullaniciDto, request.BeniHatirla);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK);
        }
    }
}
