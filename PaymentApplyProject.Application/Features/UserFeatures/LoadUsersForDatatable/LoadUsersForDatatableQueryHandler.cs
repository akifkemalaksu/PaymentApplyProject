using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.DatatableDtos;
using PaymentApplyProject.Application.Extensions;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Application.Features.UserFeatures.LoadUsersForDatatable
{
    public class LoadUsersForDatatableQueryHandler : IRequestHandler<LoadUsersForDatatableQuery, DtResult<LoadUsersForDatatableResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public LoadUsersForDatatableQueryHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<DtResult<LoadUsersForDatatableResult>> Handle(LoadUsersForDatatableQuery request, CancellationToken cancellationToken)
        {
            var users = _paymentContext.Users.Where(x =>
                (request.Active == null || x.Active == request.Active)
                && x.UserRoles.Any(ky =>
                    new short[] { RoleConstants.USER_ID, RoleConstants.ACCOUNTING_ID }.Contains(ky.Role.Id)
                    && !ky.Deleted)
                && !x.Deleted);

            var searchBy = request.Search?.Value;
            if (!string.IsNullOrEmpty(searchBy))
                users = users.Where(x =>
                    x.Username.Contains(searchBy)
                    || x.Email.Contains(searchBy)
                    || (x.Name + " " + x.Surname).Contains(searchBy)
                    || x.UserCompanies.Any(kf =>
                        kf.Company.Name.Contains(searchBy)
                        && !kf.Deleted)
                );

            var usersMapped = users.Select(x => new LoadUsersForDatatableResult
            {
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Username = x.Username,
                Companies = string.Join(',', x.UserCompanies.Where(x => !x.Deleted).Select(x => x.Company.Name).AsEnumerable()),
                Role = Names.ResourceManager.GetString(x.UserRoles.Where(x => !x.Deleted).Select(x => x.Role.Name).FirstOrDefault() ?? string.Empty),
                AddDate = x.AddDate,
                Id = x.Id,
                Active = x.Active,
            });

            var orderCriteria = "Id";
            var orderAscendingDirection = true;
            if (request.Order != null)
            {
                orderCriteria = request.Columns[request.Order[0].Column].Data;
                orderAscendingDirection = request.Order[0].Dir.ToString().ToLower() == "asc";
            }

            usersMapped = orderAscendingDirection ?
                usersMapped.OrderByDynamic(orderCriteria, DtOrderDir.Desc)
                : usersMapped.OrderByDynamic(orderCriteria, DtOrderDir.Asc);

            var filteredResultsCount = await users.CountAsync(cancellationToken);
            var totalResultsCount = await _paymentContext.Users.CountAsync(x =>
                x.UserRoles.Any(ky =>
                    new short[] { RoleConstants.USER_ID, RoleConstants.ACCOUNTING_ID }.Contains(ky.Role.Id)
                    && !ky.Deleted)
                && !x.Deleted
            , cancellationToken);

            return new DtResult<LoadUsersForDatatableResult>
            {
                Draw = request.Draw,
                RecordsFiltered = filteredResultsCount,
                RecordsTotal = totalResultsCount,
                Data = await usersMapped
                        .Skip(request.Start)
                        .Take(request.Length)
                        .ToListAsync(cancellationToken)
            };
        }
    }
}
