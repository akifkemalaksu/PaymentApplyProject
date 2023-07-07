using PaymentApplyProject.Domain.Entities;
using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Context;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.AddBankAccount
{
    public class AddBankAccountCommandHandler : IRequestHandler<AddBankAccountCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public AddBankAccountCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
        {
            var isExistSameBankaHesabi = await _paymentContext.BankaHesaplari.AnyAsync(x =>
                x.HesapNumarasi == request.HesapNumarasi
                && !x.SilindiMi, cancellationToken);
            if (isExistSameBankaHesabi)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameAccountNumber);

            var isExistsRange = await _paymentContext.BankaHesaplari.AnyAsync(x =>
                x.BankaId == request.BankaId
                && x.AltLimit <= request.AltLimit && x.UstLimit >= request.AltLimit
                && x.AltLimit <= request.UstLimit && x.UstLimit >= request.UstLimit
            , cancellationToken);
            if (isExistsRange)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPriceRange);

            var bankaHesap = _customMapper.Map<BankaHesabi>(request);
            bankaHesap.AktifMi = true;

            await _paymentContext.BankaHesaplari.AddAsync(bankaHesap, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
