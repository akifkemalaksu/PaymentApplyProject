using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Services.InfrastructureServices;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForSelect
{
    public class LoadCompaniesForSelectQueryHandler : IRequestHandler<LoadCompaniesForSelectQuery, SelectResult>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly ICustomMapper _mapper;

        public LoadCompaniesForSelectQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService, ICustomMapper mapper)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
            _mapper = mapper;
        }

        public async Task<SelectResult> Handle(LoadCompaniesForSelectQuery request, CancellationToken cancellationToken)
        {
            request.Page -= 1;

            var userInfo = _authenticatedUserService.GetUserInfo();
            var companyIds = userInfo.Companies.Select(x => x.Id).ToList();

            var companiesQuery = _paymentContext.Companies.Where(x =>
            ((!userInfo.DoesHaveUserRole() && !userInfo.DoesHaveAccountingRole()) || companyIds.Contains(x.Id))
            && !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                companiesQuery = companiesQuery.Where(x => x.Name.Contains(request.Search));

            var companiesMappedQuery = _mapper.QueryMap<Option>(companiesQuery);

            return new SelectResult
            {
                Count = await companiesMappedQuery.CountAsync(cancellationToken),
                Items = await companiesMappedQuery
                .Skip(request.Page * request.PageLength)
                .Take(request.PageLength)
                .AsNoTracking()
                .ToListAsync(cancellationToken)
            };
        }
    }
}
