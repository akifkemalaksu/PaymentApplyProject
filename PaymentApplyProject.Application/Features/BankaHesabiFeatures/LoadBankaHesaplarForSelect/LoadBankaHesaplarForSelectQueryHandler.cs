using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankaHesaplarForSelect
{
    public class LoadBankaHesaplarForSelectQueryHandler : IRequestHandler<LoadBankaHesaplarForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadBankaHesaplarForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadBankaHesaplarForSelectQuery request, CancellationToken cancellationToken)
        {
            var bankaHesaplar = _paymentContext.BankaHesaplari.Where(x =>
                (request.BankaId == 0 || x.BankaId == request.BankaId)
                && !x.SilindiMi);

            if (!string.IsNullOrEmpty(request.Search))
                bankaHesaplar = bankaHesaplar.Where(x =>
                    (x.Ad + " " + x.Soyad).Contains(request.Search)
                    || x.HesapNumarasi.Contains(request.Search)
                );

            return new SelectResult
            {
                Count = await bankaHesaplar.CountAsync(cancellationToken),
                Items = await bankaHesaplar
                    .Skip(request.Page * request.PageLength)
                    .Take(request.PageLength)
                    .Select(x => new Option
                    {
                        Text = x.Ad,
                        Id = x.Id.ToString(),
                        Disabled = !x.AktifMi
                    }).ToListAsync(cancellationToken)
            };
        }
    }
}
