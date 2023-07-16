using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Features.DepositFeatures.LoadDepositsForDatatable;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.DepositFeatures.LoadDepositsForDatatable
{
    public class LoadDepositsForDatatableQueryHandler : IRequestHandler<LoadDepositsForDatatableQuery, DtResult<LoadDepositsForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        public LoadDepositsForDatatableQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<DtResult<LoadDepositsForDatatableResult>> Handle(LoadDepositsForDatatableQuery request, CancellationToken cancellationToken)
        {
            var userInfo = _authenticatedUserService.GetUserInfo();

            var companyIds = userInfo.Companies.Select(x => x.Id).ToList();

            var deposits = _paymentContext.Deposits.Where(x =>
                (userInfo.DoesHaveUserRole() ? companyIds.Contains(x.Customer.CompanyId) : true)
                && (x.AddDate >= request.StartDate && x.AddDate <= request.EndDate)
                && (request.BankId == 0 || x.BankAccount.BankId == request.BankId)
                && (request.BankAccountId == 0 || x.BankAccountId == request.BankAccountId)
                && (request.CompanyId == 0 || x.Customer.CompanyId == request.CompanyId)
                && (request.CustomerId == 0 || x.CustomerId == request.CustomerId)
                && (request.StatusId == 0 || x.DepositStatusId == request.StatusId)
                && !x.Delete);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                deposits = deposits.Where(x =>
                    x.Customer.Company.Name.Contains(searchBy)
                    || x.Customer.Username.Contains(searchBy)
                    || (x.Customer.Name + " " + x.Customer.Surname).Contains(searchBy)
                    || x.DepositStatus.Name.Contains(searchBy)
                    || (x.BankAccount.Name + " " + x.BankAccount.Surname).Contains(searchBy)
                    || x.BankAccount.AccountNumber.Contains(searchBy)
                    || x.BankAccount.Bank.Name.Contains(searchBy));

            var depositsMapped = deposits.Select(x => new LoadDepositsForDatatableResult
            {
                Bank = x.BankAccount.Bank.Name,
                BankAccountNumber = x.BankAccount.AccountNumber,
                BankAccountOwner = x.BankAccount.Name + " " + x.BankAccount.Surname,
                Company = x.Customer.Company.Name,
                CustomerNameSurname = x.Customer.Name + " " + x.Customer.Surname,
                CustomerUsername = x.Customer.Username,
                Amount = x.Amount,
                ApprovedAmount = x.ApprovedAmount,
                Status = x.DepositStatus.Name,
                StatusId = x.DepositStatusId,
                Id = x.Id,
                TransactionDate = x.TransactionDate,
                AddDate = x.AddDate,
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            depositsMapped = orderAscendingDirection ?
                depositsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : depositsMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await deposits.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Deposits.CountAsync(x =>
            (userInfo.DoesHaveUserRole() ? companyIds.Contains(x.Customer.CompanyId) : true)
            && !x.Delete, cancellationToken);

            return new DtResult<LoadDepositsForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await depositsMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
