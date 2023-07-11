using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Context;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Extensions;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.LoadCustomersForDatatable
{
    public class LoadCustomersForDatatableQueryHandler : IRequestHandler<LoadCustomersForDatatableQuery, DtResult<LoadCustomersForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadCustomersForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadCustomersForDatatableResult>> Handle(LoadCustomersForDatatableQuery request, CancellationToken cancellationToken)
        {
            var customers = _paymentContext.Musteriler.Where(x =>
                (request.FirmaId == 0 || x.FirmaId == request.FirmaId)
                && (request.AktifMi == null || x.AktifMi == request.AktifMi)
                && !x.SilindiMi
            );

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                customers = customers.Where(x =>
                    x.KullaniciAdi.Contains(searchBy)
                    || (x.Ad + " " + x.Soyad).Contains(searchBy)
                    || x.Firma.Ad.Contains(searchBy)
                );

            var customersMapped = customers.Select(x => new LoadCustomersForDatatableResult
            {
                AdSoyad = x.Ad + " " + x.Soyad,
                AktifMi = x.AktifMi,
                Firma = x.Firma.Ad,
                KullaniciAdi = x.KullaniciAdi,
                Id = x.Id,
                EklemeTarihi = x.EklemeTarihi,
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            customersMapped = orderAscendingDirection ?
                customersMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : customersMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await customers.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Musteriler.CountAsync(x => !x.SilindiMi, cancellationToken);

            return new DtResult<LoadCustomersForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await customersMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
