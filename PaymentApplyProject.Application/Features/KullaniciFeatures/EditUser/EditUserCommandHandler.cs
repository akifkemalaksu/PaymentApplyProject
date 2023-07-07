using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.EditUser
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public EditUserCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _paymentContext.Kullanicilar
                .Include(x => x.KullaniciFirmalar)
                .FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.SilindiMi
            , cancellationToken);

            if (user == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, string.Format(Messages.NotFound, nameof(Kullanici)));

            var isExistSameUsername = await _paymentContext.Kullanicilar.AnyAsync(x =>
                x.KullaniciAdi == request.KullaniciAdi
                && x.Id != request.Id
                && !x.SilindiMi
            , cancellationToken);
            if (isExistSameUsername)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameUsername);

            var isExistSameEmail = await _paymentContext.Kullanicilar.AnyAsync(x =>
                x.Email == request.Email
                && x.Id != request.Id
                && !x.SilindiMi
            , cancellationToken);
            if (isExistSameEmail)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameEmail);

            user.Ad = request.Ad;
            user.Soyad = request.Soyad;
            user.KullaniciAdi = request.KullaniciAdi;
            user.Email = request.Email;
            user.AktifMi = request.AktifMi;
            user.KullaniciFirmalar = request.Firmalar.Select(x => new KullaniciFirma
            {
                FirmaId = x
            })
            .ToList();

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
