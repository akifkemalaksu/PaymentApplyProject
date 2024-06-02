using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Services.InfrastructureServices;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable
{
    public class LoadCustomersForDatatableQueryHandler : IRequestHandler<LoadCustomersForDatatableQuery, DtResult<LoadCustomersForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly ICustomMapper _mapper;

        public LoadCustomersForDatatableQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
            _mapper = mapper;
        }

        public async Task<DtResult<LoadCustomersForDatatableResult>> Handle(LoadCustomersForDatatableQuery request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();
            var companyIds = userInfo.Companies.Select(x => x.Id).ToList();

            var customersQuery = _paymentContext.Customers.Where(x =>
                ((!userInfo.DoesHaveUserRole() && !userInfo.DoesHaveAccountingRole()) || companyIds.Contains(x.CompanyId))
                && (request.CompanyId == 0 || x.CompanyId == request.CompanyId)
                && (request.Active == null || x.Active == request.Active)
                && !x.Deleted
            );

            var customersMappedQuery = _mapper.QueryMap<LoadCustomersForDatatableResult>(customersQuery);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                customersMappedQuery = customersMappedQuery.Where(x =>
                    x.Username.Contains(searchBy)
                    || x.NameSurname.Contains(searchBy)
                    || x.Company.Contains(searchBy)
                );

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            customersMappedQuery = orderAscendingDirection ?
                customersMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : customersMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await customersMappedQuery.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Customers.CountAsync(x => !x.Deleted, cancellationToken);

            return new DtResult<LoadCustomersForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await customersMappedQuery
                        .Skip(request.Start)
                        .Take(request.Length)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
