using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForSelect
{
    public class LoadBankAccountsForSelectQueryHandler : IRequestHandler<LoadBankAccountsForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _mapper;

        public LoadBankAccountsForSelectQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<SelectResult> Handle(LoadBankAccountsForSelectQuery request, CancellationToken cancellationToken)
        {
            request.Page -= 1;

            var bankAccountsQuery = _paymentContext.BankAccounts.Where(x =>
                (request.BankId == 0 || x.BankId == request.BankId)
                && !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                bankAccountsQuery = bankAccountsQuery.Where(x =>
                    (x.Bank.Name + " - " + x.Name + " " + x.Surname + " - " + x.AccountNumber).Contains(request.Search)
                    || x.AccountNumber.Contains(request.Search)
                );

            var bankAccountsMappedQuery = _mapper.QueryMap<Option>(bankAccountsQuery);

            return new SelectResult
            {
                Count = await bankAccountsMappedQuery.CountAsync(cancellationToken),
                Items = await bankAccountsMappedQuery
                    .Skip(request.Page * request.PageLength)
                    .Take(request.PageLength)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
