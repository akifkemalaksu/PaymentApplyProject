using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Mapping;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankAccountById
{
    public class GetBankAccountByIdQueryHandler : IRequestHandler<GetBankAccountByIdQuery, Response<GetBankAccountByIdResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public GetBankAccountByIdQueryHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<GetBankAccountByIdResult>> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var bankAccount = await _paymentContext.BankaHesaplari
                .Where(x =>
                    x.Id == request.Id
                    && !x.SilindiMi)
                .Select(x => new GetBankAccountByIdResult
                {
                    Ad = x.Ad,
                    AktifMi = x.AktifMi,
                    AltLimit = x.AltLimit,
                    Banka = x.Banka.Ad,
                    BankaId = x.BankaId,
                    HesapNumarasi = x.HesapNumarasi,
                    Id = request.Id,
                    Soyad = x.Soyad,
                    UstLimit = x.UstLimit
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (bankAccount is null)
                return Response<GetBankAccountByIdResult>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            return Response<GetBankAccountByIdResult>.Success(System.Net.HttpStatusCode.OK, bankAccount);
        }
    }
}
