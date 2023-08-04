using MediatR;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Context;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable
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
            var customers = _paymentContext.Customers.Where(x =>
                (request.CompanyId == 0 || x.CompanyId == request.CompanyId)
                && (request.Active == null || x.Active == request.Active)
                && !x.Deleted
            );

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                customers = customers.Where(x =>
                    x.Username.Contains(searchBy)
                    || (x.Name + " " + x.Surname).Contains(searchBy)
                    || x.Company.Name.Contains(searchBy)
                );

            var customersMapped = customers.Select(x => new LoadCustomersForDatatableResult
            {
                NameSurname = x.Name + " " + x.Surname,
                Active = x.Active,
                Company = x.Company.Name,
                Username = x.Username,
                Id = x.Id,
                AddDate = x.AddDate,
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
            var totalResultsCount = await _paymentContext.Customers.CountAsync(x => !x.Deleted, cancellationToken);

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
