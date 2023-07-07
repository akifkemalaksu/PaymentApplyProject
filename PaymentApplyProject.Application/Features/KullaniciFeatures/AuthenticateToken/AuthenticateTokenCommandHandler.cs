﻿using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Dtos.KullaniciDtos;

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
            var kullanici = await _paymentContext.Kullanicilar.FirstOrDefaultAsync(x =>
                    x.KullaniciAdi == request.KullaniciAdi
                    && x.Sifre == request.Sifre
                    && !x.SilindiMi,cancellationToken);
            if (kullanici == null)
                return Response<AuthenticateTokenResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            var yetkiler = await _paymentContext.Yetkiler.Where(x =>
                x.KullaniciYetkiler.Any(ky =>
                    ky.KullaniciId == kullanici.Id
                    && !ky.SilindiMi)
                && !x.SilindiMi).Select(x => x.Ad).ToArrayAsync(cancellationToken);

            var kullaniciDto = _customMapper.Map<KullaniciDto>(kullanici);
            kullaniciDto.Yetkiler = yetkiler;

            return _jwtTokenService.CreateToken(kullaniciDto);
        }
    }
}
