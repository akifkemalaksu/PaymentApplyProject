using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForSelect;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForSelect
{
    public class LoadBanksForSelectQueryHandler : IRequestHandler<LoadBanksForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadBanksForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadBanksForSelectQuery request, CancellationToken cancellationToken)
        {
            var banks = _paymentContext.Banks.Where(x => !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                banks = banks.Where(x => x.Name.Contains(request.Search));

            return new SelectResult
            {
                Count = await banks.CountAsync(cancellationToken),
                Items = await banks.Skip(request.Page * request.PageLength).Take(request.PageLength).Select(x => new Option
                {
                    Text = x.Name,
                    Id = x.Id.ToString()
                }).ToListAsync(cancellationToken)
            };
        }
    }
}
