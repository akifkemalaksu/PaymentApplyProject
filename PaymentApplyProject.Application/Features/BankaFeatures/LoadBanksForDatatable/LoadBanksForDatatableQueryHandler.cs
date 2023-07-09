using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;

namespace PaymentApplyProject.Application.Features.BankaFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableQueryHandler : IRequestHandler<LoadBanksForDatatableQuery, DtResult<LoadBanksForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadBanksForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadBanksForDatatableResult>> Handle(LoadBanksForDatatableQuery request, CancellationToken cancellationToken)
        {
            var banks = _paymentContext.Bankalar.Where(x => !x.SilindiMi);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                banks = banks.Where(x =>
                    x.Id.ToString().Contains(searchBy)
                    || x.Ad.Contains(searchBy)
                );

            var banksMapped = banks.Select(x => new LoadBanksForDatatableResult
            {
                Id = x.Id,
                Ad = x.Ad,
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            banksMapped = orderAscendingDirection ?
                banksMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : banksMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await banks.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Bankalar.CountAsync(x => !x.SilindiMi, cancellationToken);

            return new DtResult<LoadBanksForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await banksMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
