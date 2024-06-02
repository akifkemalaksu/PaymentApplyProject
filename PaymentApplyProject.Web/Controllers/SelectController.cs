using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Attributes;
using PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForSelect;
using PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForSelect;
using PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForSelect;
using PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomerForSelect;

namespace PaymentApplyProject.Web.Controllers
{
    [AdvancedAuthorize(Roles = "admin,user,accounting")]
    public class SelectController : Controller
    {
        private readonly IMediator _mediator;

        public SelectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Companies(LoadCompaniesForSelectQuery loadFirmalarForSelectQuery)
        {
            var result = await _mediator.Send(loadFirmalarForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Customers(LoadCustomerForSelectQuery loadMusterilerForSelectQuery)
        {
            var result = await _mediator.Send(loadMusterilerForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Banks(LoadBanksForSelectQuery loadBankalarForSelectQuery)
        {
            var result = await _mediator.Send(loadBankalarForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> BankAccounts(LoadBankAccountsForSelectQuery loadBankaHesaplarForSelectQuery)
        {
            var result = await _mediator.Send(loadBankaHesaplarForSelectQuery);
            return Json(result);
        }
    }
}
