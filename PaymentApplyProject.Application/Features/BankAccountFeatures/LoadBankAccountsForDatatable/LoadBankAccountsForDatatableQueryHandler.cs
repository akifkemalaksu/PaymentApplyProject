using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Interfaces;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForDatatable
{
    public class LoadBankAccountsForDatatableQueryHandler : IRequestHandler<LoadBankAccountsForDatatableQuery, DtResult<LoadBankAccountsForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadBankAccountsForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadBankAccountsForDatatableResult>> Handle(LoadBankAccountsForDatatableQuery request, CancellationToken cancellationToken)
        {
            var bankAccounts = _paymentContext.BankAccounts.Where(x =>
                (request.BankId == 0 || x.BankId == request.BankId)
                && (request.Active == null || x.Active == request.Active)
                && (request.Amount == 0 || (request.Amount >= x.LowerLimit && request.Amount <= x.UpperLimit))
                && !x.Deleted);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                bankAccounts = bankAccounts.Where(x =>
                    x.Bank.Name.Contains(searchBy)
                    || x.AccountNumber.Contains(searchBy)
                    || (x.Name + " " + x.Surname).Contains(searchBy));

            var bankAccountsMapped = bankAccounts.Select(x => new LoadBankAccountsForDatatableResult
            {
                AccountNumber = x.AccountNumber,
                Bank = x.Bank.Name,
                NameSurname = x.Name + " " + x.Surname,
                LowerLimit = x.LowerLimit,
                UpperLimit = x.UpperLimit,
                Active = x.Active,
                Id = x.Id,
                AddDate = x.AddDate
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            bankAccountsMapped = orderAscendingDirection ?
                bankAccountsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : bankAccountsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await bankAccounts.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.BankAccounts.CountAsync(x => !x.Deleted, cancellationToken);

            return new DtResult<LoadBankAccountsForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await bankAccountsMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
