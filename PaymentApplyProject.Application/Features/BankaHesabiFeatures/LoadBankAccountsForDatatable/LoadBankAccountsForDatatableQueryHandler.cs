using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Extensions;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable
{
    public class LoadBankAccountsForDatatableQueryHandler : IRequestHandler<LoadBankAccountsForDatatableQuery, DtResult<LoadBankAccountsForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadBankAccountsForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadBankAccountsForDatatableResult>> Handle(LoadBankAccountsForDatatableQuery request, CancellationToken cancellationToken)
        {
            var bankAccounts = _paymentContext.BankaHesaplari.Where(x =>
                (request.BankaId == 0 || x.BankaId == request.BankaId)
                && (request.AktifMi == null || x.AktifMi == request.AktifMi)
                && (request.Tutar == 0 || (request.Tutar >= x.AltLimit && request.Tutar <= x.UstLimit))
                && !x.SilindiMi);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                bankAccounts = bankAccounts.Where(x =>
                    x.Banka.Ad.Contains(searchBy)
                    || x.HesapNumarasi.Contains(searchBy)
                    || (x.Ad + " " + x.Soyad).Contains(searchBy));

            var bankAccountsMapped = bankAccounts.Select(x => new LoadBankAccountsForDatatableResult
            {
                HesapNumarasi = x.HesapNumarasi,
                Banka = x.Banka.Ad,
                AdSoyad = x.Ad + " " + x.Soyad,
                AltLimit = x.AltLimit,
                UstLimit = x.UstLimit,
                AktifMi = x.AktifMi,
                Id = x.Id,
                EklemeTarihi = x.EklemeTarihi.ToString("dd.MM.yy hh:mm")
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            bankAccountsMapped = orderAscendingDirection ?
                bankAccountsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : bankAccountsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await bankAccounts.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.ParaYatirmalar.CountAsync(x => !x.SilindiMi, cancellationToken);

            return new DtResult<LoadBankAccountsForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await bankAccountsMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
