using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableQueryHandler : IRequestHandler<LoadBanksForDatatableQuery, DtResult<LoadBanksForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _mapper;

        public LoadBanksForDatatableQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<DtResult<LoadBanksForDatatableResult>> Handle(LoadBanksForDatatableQuery request, CancellationToken cancellationToken)
        {
            var banksQuery = _paymentContext.Banks.Where(x => !x.Deleted);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                banksQuery = banksQuery.Where(x =>
                    x.Id.ToString().Contains(searchBy)
                    || x.Name.Contains(searchBy)
                );

            var banksMappedQuery = _mapper.QueryMap<LoadBanksForDatatableResult>(banksQuery);

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            banksMappedQuery = orderAscendingDirection ?
                banksMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : banksMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await banksMappedQuery.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Banks.CountAsync(x => !x.Deleted, cancellationToken);

            return new DtResult<LoadBanksForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await banksMappedQuery
                        .Skip(request.Start)
                        .Take(request.Length)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
