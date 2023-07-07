using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.BankaFeatures.LoadBankalarForSelect
{
    public class LoadBankalarForSelectQueryHandler : IRequestHandler<LoadBankalarForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadBankalarForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadBankalarForSelectQuery request, CancellationToken cancellationToken)
        {
            var bankalar = _paymentContext.Bankalar.Where(x => !x.SilindiMi);

            if (!string.IsNullOrEmpty(request.Search))
                bankalar = bankalar.Where(x => x.Ad.Contains(request.Search));

            return new SelectResult
            {
                Count = await bankalar.CountAsync(cancellationToken),
                Items = await bankalar.Skip(request.Page * request.PageLength).Take(request.PageLength).Select(x => new Option
                {
                    Text = x.Ad,
                    Id = x.Id.ToString()
                }).ToListAsync(cancellationToken)
            };
        }
    }
}
