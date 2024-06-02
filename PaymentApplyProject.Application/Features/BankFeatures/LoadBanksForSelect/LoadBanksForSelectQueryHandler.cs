using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForSelect
{
    public class LoadBanksForSelectQueryHandler : IRequestHandler<LoadBanksForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _mapper;

        public LoadBanksForSelectQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<SelectResult> Handle(LoadBanksForSelectQuery request, CancellationToken cancellationToken)
        {
            request.Page -= 1;

            var banksQuery = _paymentContext.Banks.Where(x => !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                banksQuery = banksQuery.Where(x => x.Name.Contains(request.Search));

            var banksMappedQuery = _mapper.QueryMap<Option>(banksQuery).AsNoTracking();

            return new SelectResult
            {
                Count = await banksMappedQuery.CountAsync(cancellationToken),
                Items = await banksMappedQuery
                        .Skip(request.Page * request.PageLength)
                        .Take(request.PageLength)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
