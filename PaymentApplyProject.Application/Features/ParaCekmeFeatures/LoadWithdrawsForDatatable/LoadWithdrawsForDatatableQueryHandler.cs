using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.LoadWithdrawsForDatatable
{
    public class LoadWithdrawsForDatatableQueryHandler : IRequestHandler<LoadWithdrawsForDatatableQuery, DtResult<LoadWithdrawsForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadWithdrawsForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadWithdrawsForDatatableResult>> Handle(LoadWithdrawsForDatatableQuery request, CancellationToken cancellationToken)
        {
            var withdraws = _paymentContext.ParaCekmeler.Where(x =>
                (request.FirmaId == 0 || x.Musteri.FirmaId == request.FirmaId)
                && (request.MusteriId == 0 || x.MusteriId == request.MusteriId)
                && (request.DurumId == 0 || x.ParaCekmeDurumId == request.DurumId)
                && !x.SilindiMi);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                withdraws = withdraws.Where(x =>
                    x.Musteri.Firma.Ad.Contains(searchBy)
                    || x.Musteri.KullaniciAdi.Contains(searchBy)
                    || (x.Musteri.Ad + " " + x.Musteri.Soyad).Contains(searchBy)
                    || x.HesapNumarasi.Contains(searchBy));

            var withdrawsMapped = withdraws.Select(x => new LoadWithdrawsForDatatableResult
            {
                BankaHesapNo = x.HesapNumarasi,
                Firma = x.Musteri.Firma.Ad,
                MusteriAdSoyad = x.Musteri.Ad + " " + x.Musteri.Soyad,
                MusteriKullaniciAd = x.Musteri.KullaniciAdi,
                OnaylananTutar = x.OnaylananTutar,
                IslemTarihi = x.IslemTarihi != null ? x.IslemTarihi.Value.ToString("dd.MM.yy hh:mm") : string.Empty,
                TalepTarihi = x.EklemeTarihi.ToString("dd.MM.yy hh:mm"),
                Durum = x.ParaCekmeDurum.Ad,
                DurumId = x.ParaCekmeDurumId,
                Tutar = x.Tutar,
                Id = x.Id,
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            withdrawsMapped = orderAscendingDirection ?
                withdrawsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : withdrawsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await withdraws.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.ParaCekmeler.CountAsync(x => !x.SilindiMi, cancellationToken);

            return new DtResult<LoadWithdrawsForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await withdrawsMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
