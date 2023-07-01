using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable
{
    public class LoadDepositsForDatatableQueryHandler : IRequestHandler<LoadDepositsForDatatableQuery, DtResult<LoadDepositsForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadDepositsForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadDepositsForDatatableResult>> Handle(LoadDepositsForDatatableQuery request, CancellationToken cancellationToken)
        {
            var deposits = _paymentContext.ParaYatirmalar.Where(x =>
                (request.BankaId == 0 || x.BankaHesabi.BankaId == request.BankaId)
                && (request.BankaHesapId == 0 || x.BankaHesabiId == request.BankaHesapId)
                && (request.FirmaId == 0 || x.Musteri.FirmaId == request.FirmaId)
                && (request.MusteriId == 0 || x.MusteriId == request.MusteriId)
                && (request.DurumId == 0 || x.ParaYatirmaDurumId == request.DurumId)
                && !x.SilindiMi);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                deposits = deposits.Where(x =>
                    x.Musteri.Firma.Ad.Contains(searchBy)
                    || x.Musteri.KullaniciAdi.Contains(searchBy)
                    || (x.Musteri.Ad + " " + x.Musteri.Soyad).Contains(searchBy)
                    || x.ParaYatirmaDurum.Ad.Contains(searchBy)
                    || (x.BankaHesabi.Ad + " " + x.BankaHesabi.Soyad).Contains(searchBy)
                    || x.BankaHesabi.HesapNumarasi.Contains(searchBy)
                    || x.BankaHesabi.Banka.Ad.Contains(searchBy));

            var depositsMapped = deposits.Select(x => new LoadDepositsForDatatableResult
            {
                Banka = x.BankaHesabi.Banka.Ad,
                BankaHesapNo = x.BankaHesabi.HesapNumarasi,
                BankaHesapSahibi = x.BankaHesabi.Ad + " " + x.BankaHesabi.Soyad,
                Firma = x.Musteri.Firma.Ad,
                MusteriAdSoyad = x.Musteri.Ad + " " + x.Musteri.Soyad,
                MusteriKullaniciAd = x.Musteri.KullaniciAdi,
                Tutar = x.Tutar,
                OnaylananTutar = x.OnaylananTutar,
                Durum = x.ParaYatirmaDurum.Ad,
                DurumId = x.ParaYatirmaDurumId,
                Id = x.Id,
                IslemTarihi = x.IslemTarihi != null ? x.IslemTarihi.Value.ToString("dd.MM.yy hh:mm") : string.Empty,
                TalepTarihi = x.EklemeTarihi.ToString("dd.MM.yy hh:mm"),
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            depositsMapped = orderAscendingDirection ?
                depositsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : depositsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await deposits.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.ParaYatirmalar.CountAsync(x => !x.SilindiMi, cancellationToken);

            return new DtResult<LoadDepositsForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await depositsMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
