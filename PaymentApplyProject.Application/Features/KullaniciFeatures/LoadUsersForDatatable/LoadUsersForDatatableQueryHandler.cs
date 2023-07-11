using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.LoadUsersForDatatable
{
    public class LoadUsersForDatatableQueryHandler : IRequestHandler<LoadUsersForDatatableQuery, DtResult<LoadUsersForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadUsersForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadUsersForDatatableResult>> Handle(LoadUsersForDatatableQuery request, CancellationToken cancellationToken)
        {
            var users = _paymentContext.Kullanicilar.Where(x =>
                (request.AktifMi == null || x.AktifMi == request.AktifMi)
                && x.KullaniciYetkiler.Any(ky =>
                    ky.Yetki.Id == RolSabitler.USER_ID
                    && !ky.SilindiMi)
                && !x.SilindiMi);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                users = users.Where(x =>
                    x.KullaniciAdi.Contains(searchBy)
                    || x.Email.Contains(searchBy)
                    || (x.Ad + " " + x.Soyad).Contains(searchBy)
                    || x.KullaniciFirmalar.Any(kf =>
                        kf.Firma.Ad.Contains(searchBy)
                        && !kf.SilindiMi)
                );

            var usersMapped = users.Select(x => new LoadUsersForDatatableResult
            {
                Ad = x.Ad,
                Soyad = x.Soyad,
                Email = x.Email,
                KullaniciAdi = x.KullaniciAdi,
                Firmalar = string.Join(',', x.KullaniciFirmalar.Where(x => !x.SilindiMi).Select(x => x.Firma.Ad).AsEnumerable()),
                EklemeTarihi = x.EklemeTarihi,
                Id = x.Id,
                AktifMi = x.AktifMi,
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            usersMapped = orderAscendingDirection ?
                usersMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : usersMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await users.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Kullanicilar.CountAsync(x =>
                x.KullaniciYetkiler.Any(ky =>
                    ky.Yetki.Id == RolSabitler.USER_ID
                    && !ky.SilindiMi)
                && !x.SilindiMi
            , cancellationToken);

            return new DtResult<LoadUsersForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await usersMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
