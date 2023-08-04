using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForSelect;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForSelect
{
    public class LoadCompaniesForSelectQueryHandler : IRequestHandler<LoadCompaniesForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadCompaniesForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadCompaniesForSelectQuery request, CancellationToken cancellationToken)
        {
            var companies = _paymentContext.Companies.Where(x => !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                companies = companies.Where(x => x.Name.Contains(request.Search));

            return new SelectResult
            {
                Count = await companies.CountAsync(cancellationToken),
                Items = await companies.Skip(request.Page * request.PageLength).Take(request.PageLength).Select(x => new Option
                {
                    Text = x.Name,
                    Id = x.Id.ToString()
                }).ToListAsync(cancellationToken)
            };
        }
    }
}
