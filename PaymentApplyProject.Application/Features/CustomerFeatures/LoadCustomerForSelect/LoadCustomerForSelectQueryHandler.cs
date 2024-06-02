using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomerForSelect
{
    public class LoadCustomerForSelectQueryHandler : IRequestHandler<LoadCustomerForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _mapper;

        public LoadCustomerForSelectQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<SelectResult> Handle(LoadCustomerForSelectQuery request, CancellationToken cancellationToken)
        {
            request.Page -= 1;

            var customersQuery = _paymentContext.Customers.Where(x =>
                (request.CompanyId == 0 || x.CompanyId == request.CompanyId)
                && !x.Deleted);

            var customerMappedQuery = _mapper.QueryMap<Option>(customersQuery);

            if (!string.IsNullOrEmpty(request.Search))
                customerMappedQuery = customerMappedQuery.Where(x =>
                    (x.Text).Contains(request.Search));

            return new SelectResult
            {
                Count = await customerMappedQuery.CountAsync(cancellationToken),
                Items = await customerMappedQuery
                    .Skip(request.Page * request.PageLength)
                    .Take(request.PageLength)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
