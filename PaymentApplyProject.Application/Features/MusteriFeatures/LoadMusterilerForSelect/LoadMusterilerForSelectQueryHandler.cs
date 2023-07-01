using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.LoadMusterilerForSelect
{
    public class LoadMusterilerForSelectQueryHandler : IRequestHandler<LoadMusterilerForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadMusterilerForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadMusterilerForSelectQuery request, CancellationToken cancellationToken)
        {
            request.Search ??= string.Empty;
            var musteriler = _paymentContext.Musteriler.Where(x =>
                (request.FirmaId == 0 || x.FirmaId == request.FirmaId)
                && (
                    (x.Ad + " " + x.Soyad).Contains(request.Search)
                    || x.KullaniciAdi.Contains(request.Search)
                )
                && !x.SilindiMi);

            return new SelectResult
            {
                Count = await musteriler.CountAsync(cancellationToken),
                Items = await musteriler
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
