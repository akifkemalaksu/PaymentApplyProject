using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable
{
    public class LoadCompaniesForDatatableQueryHandler : IRequestHandler<LoadCompaniesForDatatableQuery, DtResult<LoadCompaniesForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadCompaniesForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadCompaniesForDatatableResult>> Handle(LoadCompaniesForDatatableQuery request, CancellationToken cancellationToken)
        {
            var companies = _paymentContext.Companies.Where(x =>
            (request.Active == null || x.Active == request.Active)
            && !x.Deleted);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                companies = companies.Where(x =>
                    x.Id.ToString().Contains(searchBy)
                    || x.Name.Contains(searchBy)
                );

            var companiesMapped = companies.Select(x => new LoadCompaniesForDatatableResult
            {
                Id = x.Id,
                Name = x.Name,
                Active = x.Active
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            companiesMapped = orderAscendingDirection ?
                companiesMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : companiesMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await companies.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Companies.CountAsync(x => !x.Deleted, cancellationToken);

            return new DtResult<LoadCompaniesForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await companiesMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
