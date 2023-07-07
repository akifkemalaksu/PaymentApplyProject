using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Helpers;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public AddUserCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var isExistSameUsername = await _paymentContext.Kullanicilar.AnyAsync(x => x.KullaniciAdi == request.KullaniciAdi && !x.SilindiMi, cancellationToken);
            if (isExistSameUsername)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameUsername);

            var isExistSameEmail = await _paymentContext.Kullanicilar.AnyAsync(x => x.Email == request.Email && !x.SilindiMi, cancellationToken);
            if (isExistSameEmail)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameEmail);

            var sifre = PasswordGenerator.GeneratePassword();
            var kullanici = _customMapper.Map<Kullanici>(request);
            kullanici.Sifre = sifre;
            kullanici.AktifMi = true;
            kullanici.KullaniciFirmalar = request.Firmalar.Select(x => new KullaniciFirma
            {
                FirmaId = x,
            }).ToList();
            kullanici.KullaniciYetkiler = new List<KullaniciYetki> { new()
            {
                YetkiId = RolSabitler.USER_ID
            }};

            await _paymentContext.Kullanicilar.AddAsync(kullanici, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
