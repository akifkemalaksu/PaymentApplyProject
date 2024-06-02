﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable
{
    public class LoadCompaniesForDatatableQueryHandler : IRequestHandler<LoadCompaniesForDatatableQuery, DtResult<LoadCompaniesForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _mapper;

        public LoadCompaniesForDatatableQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<DtResult<LoadCompaniesForDatatableResult>> Handle(LoadCompaniesForDatatableQuery request, CancellationToken cancellationToken)
        {
            var companiesQuery = _paymentContext.Companies.Where(x =>
            (request.Active == null || x.Active == request.Active)
            && !x.Deleted);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                companiesQuery = companiesQuery.Where(x =>
                    x.Id.ToString().Contains(searchBy)
                    || x.Name.Contains(searchBy)
                );

            var companiesMappedQuery = _mapper.QueryMap<LoadCompaniesForDatatableResult>(companiesQuery);

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            companiesMappedQuery = orderAscendingDirection ?
                companiesMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : companiesMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await companiesMappedQuery.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Companies.CountAsync(x => !x.Deleted, cancellationToken);

            return new DtResult<LoadCompaniesForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await companiesMappedQuery
                        .Skip(request.Start)
                        .Take(request.Length)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
