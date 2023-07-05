using PaymentApplyProject.Domain.Entities;
using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Context;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.AddBankAccount
{
    public class AddBankAccountCommandHandler : IRequestHandler<AddBankAccountCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public AddBankAccountCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
        {
            var isExistSameBankaHesabi = await _paymentContext.BankaHesaplari.AnyAsync(x => x.HesapNumarasi == request.HesapNumarasi && !x.SilindiMi, cancellationToken);
            if (isExistSameBankaHesabi)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameAccountNumber);

            BankaHesabi bankaHesap = new()
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                HesapNumarasi = request.HesapNumarasi,
                BankaId = request.BankaId,
                AltLimit = request.AltLimit,
                UstLimit = request.UstLimit,
                AktifMi = true
            };

            await _paymentContext.BankaHesaplari.AddAsync(bankaHesap, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
