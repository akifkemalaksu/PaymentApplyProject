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
        private readonly ICustomMapper _mapper;

        public LoadBankAccountsForDatatableQueryHandler(IPaymentContext paymentContext, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _mapper = mapper;
        }

        public async Task<DtResult<LoadBankAccountsForDatatableResult>> Handle(LoadBankAccountsForDatatableQuery request, CancellationToken cancellationToken)
        {
            var bankAccountsQuery = _paymentContext.BankAccounts.Where(x =>
                (request.BankId == 0 || x.BankId == request.BankId)
                && (request.Active == null || x.Active == request.Active)
                && (request.Amount == 0 || (request.Amount >= x.LowerLimit && request.Amount <= x.UpperLimit))
                && !x.Deleted);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                bankAccountsQuery = bankAccountsQuery.Where(x =>
                    x.Bank.Name.Contains(searchBy)
                    || x.AccountNumber.Contains(searchBy)
                    || (x.Name + " " + x.Surname).Contains(searchBy));

            var bankAccountsMappedQuery = _mapper.QueryMap<LoadBankAccountsForDatatableResult>(bankAccountsQuery);

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            bankAccountsMappedQuery = orderAscendingDirection ?
                bankAccountsMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : bankAccountsMappedQuery.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await bankAccountsMappedQuery.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.BankAccounts.CountAsync(x => !x.Deleted, cancellationToken);

            return new DtResult<LoadBankAccountsForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await bankAccountsMappedQuery
                        .Skip(request.Start)
                        .Take(request.Length)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
