using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Features.WithdrawFeatures.LoadWithdrawsForDatatable;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.LoadWithdrawsForDatatable
{
    public class LoadWithdrawsForDatatableQueryHandler : IRequestHandler<LoadWithdrawsForDatatableQuery, DtResult<LoadWithdrawsForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadWithdrawsForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadWithdrawsForDatatableResult>> Handle(LoadWithdrawsForDatatableQuery request, CancellationToken cancellationToken)
        {
            var withdraws = _paymentContext.Withdraws.Where(x =>
                (request.CompanyId == 0 || x.Customer.CompanyId == request.CompanyId)
                && (request.CustomerId == 0 || x.CustomerId == request.CustomerId)
                && (request.StatusId == 0 || x.StatusId == request.StatusId)
                && !x.Delete);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                withdraws = withdraws.Where(x =>
                    x.Customer.Company.Name.Contains(searchBy)
                    || x.Customer.Username.Contains(searchBy)
                    || (x.Customer.Name + " " + x.Customer.Surname).Contains(searchBy)
                    || x.AccountNumber.Contains(searchBy));

            var withdrawsMapped = withdraws.Select(x => new LoadWithdrawsForDatatableResult
            {
                AccountNumber = x.AccountNumber,
                Company = x.Customer.Company.Name,
                NameSurname = x.Customer.Name + " " + x.Customer.Surname,
                Username = x.Customer.Username,
                ApprovedAmount = x.ApprovedAmount,
                TransactionDate = x.TransactionDate,
                AddDate = x.AddDate,
                Status = x.WithdrawStatus.Name,
                StatusId = x.StatusId,
                Amount = x.Amount,
                Id = x.Id,
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            withdrawsMapped = orderAscendingDirection ?
                withdrawsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : withdrawsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await withdraws.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Withdraws.CountAsync(x => !x.Delete, cancellationToken);

            return new DtResult<LoadWithdrawsForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await withdrawsMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
