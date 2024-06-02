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

        public LoadCompaniesForSelectQueryHandler(IPaymentContext paymentContext, IAuthenticatedUserService authenticatedUserService)
        {
            _paymentContext = paymentContext;
            _authenticatedUserService = authenticatedUserService;
        }

        public async Task<SelectResult> Handle(LoadCompaniesForSelectQuery request, CancellationToken cancellationToken)
        {
            request.Page -= 1;

            var userInfo = _authenticatedUserService.GetUserInfo();
            var companyIds = userInfo.Companies.Select(x => x.Id).ToList();

            var companies = _paymentContext.Companies.Where(x =>
            ((!userInfo.DoesHaveUserRole() && !userInfo.DoesHaveAccountingRole()) || companyIds.Contains(x.Id))
            && !x.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
                companies = companies.Where(x => x.Name.Contains(request.Search));

            return new SelectResult
            {
                Count = await companies.CountAsync(cancellationToken),
                Items = await companies.Skip(request.Page * request.PageLength).Take(request.PageLength).Select(x => new Option
                {
                    Text = x.Name,
                    Id = x.Id.ToString()
                }).ToListAsync(cancellationToken)
            };
        }
    }
}
