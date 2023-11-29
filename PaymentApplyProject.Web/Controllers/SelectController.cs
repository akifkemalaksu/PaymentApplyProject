using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForSelect;
using PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForSelect;
using PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForSelect;
using PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomerForSelect;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin,user,accounting")]
    public class SelectController : Controller
    {
        private readonly IMediator _mediator;

        public SelectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache", VaryByQueryKeys = new string[] { "Search" })]
        public async Task<IActionResult> Companies(LoadCompaniesForSelectQuery loadFirmalarForSelectQuery)
        {
            var result = await _mediator.Send(loadFirmalarForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache", VaryByQueryKeys = new string[] { "CompanyId", "Search" })]
        public async Task<IActionResult> Customers(LoadCustomerForSelectQuery loadMusterilerForSelectQuery)
        {
            var result = await _mediator.Send(loadMusterilerForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache", VaryByQueryKeys = new string[] { "Search" })]
        public async Task<IActionResult> Banks(LoadBanksForSelectQuery loadBankalarForSelectQuery)
        {
            var result = await _mediator.Send(loadBankalarForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache", VaryByQueryKeys = new string[] { "BankId", "Search" })]
        public async Task<IActionResult> BankAccounts(LoadBankAccountsForSelectQuery loadBankaHesaplarForSelectQuery)
        {
            var result = await _mediator.Send(loadBankaHesaplarForSelectQuery);
            return Json(result);
        }
    }
}
