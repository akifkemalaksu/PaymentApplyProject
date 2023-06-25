using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Mapping;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.Authenticate
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticateResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ICustomMapper _customMapper;

        public AuthenticateCommandHandler(IPaymentContext paymentContext, IJwtTokenService jwtTokenService, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _jwtTokenService = jwtTokenService;
            _customMapper = customMapper;
        }

        public async Task<Response<AuthenticateResult>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var kullanici = await _paymentContext.Kullanicilar.FirstOrDefaultAsync(x =>
                    x.KullaniciAdi == request.KullaniciAdi
                    && x.Sifre == request.Sifre
                    && !x.SilindiMi);
            if (kullanici == null)
                Response<AuthenticateResult>.Error(System.Net.HttpStatusCode.NotFound, string.Format(Messages.NotFoundWithName, nameof(Kullanici)));

            var yetkiler = await _paymentContext.Yetkiler.Where(x =>
                x.KullaniciYetkiler.Any(ky =>
                    ky.KullaniciId == kullanici.Id
                    && !ky.SilindiMi)
                && !x.SilindiMi).Select(x => x.Ad).ToArrayAsync();

            var kullaniciDto = _customMapper.Map<KullaniciDto>(kullanici);
            kullaniciDto.Yetkiler = yetkiler;

            return _jwtTokenService.CreateToken(kullaniciDto);
        }
    }
}
