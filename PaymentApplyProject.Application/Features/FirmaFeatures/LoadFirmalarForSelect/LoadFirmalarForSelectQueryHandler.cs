using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.FirmaFeatures.LoadFirmalarForSelect
{
    public class LoadFirmalarForSelectQueryHandler : IRequestHandler<LoadFirmalarForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadFirmalarForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadFirmalarForSelectQuery request, CancellationToken cancellationToken)
        {
            request.Search ??= string.Empty;
            var firmalar = _paymentContext.Firmalar.Where(x => x.Ad.Contains(request.Search) && !x.SilindiMi);

            return new SelectResult
            {
                Count = await firmalar.CountAsync(cancellationToken),
                Items = await firmalar.Skip(request.Page * request.PageLength).Take(request.PageLength).Select(x => new Option
                {
                    Text = x.Ad,
                    Id = x.Id.ToString()
                }).ToListAsync(cancellationToken)
            };
        }
    }
}
