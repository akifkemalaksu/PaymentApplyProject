using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForSelect;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForSelect
{
    public class LoadBankAccountsForSelectQueryHandler : IRequestHandler<LoadBankAccountsForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadBankAccountsForSelectQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<SelectResult> Handle(LoadBankAccountsForSelectQuery request, CancellationToken cancellationToken)
        {
            var bankaHesaplar = _paymentContext.BankAccounts.Where(x =>
                (request.BankId == 0 || x.BankId == request.BankId)
                && !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                bankaHesaplar = bankaHesaplar.Where(x =>
                    (x.Name + " " + x.Surname + " - " + x.AccountNumber).Contains(request.Search)
                    || x.AccountNumber.Contains(request.Search)
                );

            return new SelectResult
            {
                Count = await bankaHesaplar.CountAsync(cancellationToken),
                Items = await bankaHesaplar
                    .Skip(request.Page * request.PageLength)
                    .Take(request.PageLength)
                    .Select(x => new Option
                    {
                        Text = $"{x.Name} {x.Surname} - {x.AccountNumber}",
                        Id = x.Id.ToString(),
                        Disabled = !x.Active
                    }).ToListAsync(cancellationToken)
            };
        }
    }
}
