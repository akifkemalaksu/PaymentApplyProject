﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomerForSelect;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomerForSelect
{
    public class LoadCustomerForSelectQueryHandler : IRequestHandler<LoadCustomerForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadCustomerForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadCustomerForSelectQuery request, CancellationToken cancellationToken)
        {
            var customers = _paymentContext.Customers.Where(x =>
                (request.CompanyId == 0 || x.CompanyId == request.CompanyId)
                && !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                customers = customers.Where(x =>
                    (x.Name + " " + x.Surname).Contains(request.Search));

            return new SelectResult
            {
                Count = await customers.CountAsync(cancellationToken),
                Items = await customers
                    .Skip(request.Page * request.PageLength)
                    .Take(request.PageLength)
                    .Select(x => new Option
                    {
                        Text = $"{x.Name} {x.Surname}",
                        Id = x.Id.ToString(),
                        Disabled = !x.Active
                    }).ToListAsync(cancellationToken)
            };
        }
    }
}
