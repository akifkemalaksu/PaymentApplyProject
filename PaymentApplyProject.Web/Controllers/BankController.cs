using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable;
using System.Data;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class BankController : Controller
    {
        private readonly IMediator _mediator;

        public BankController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BankAccounts()
        {
            return View();
        }

        public async Task<IActionResult> LoadBankAccounts(LoadBankAccountsForDatatableQuery loadBankAccountsForDatatableQuery)
        {
            var result = await _mediator.Send(loadBankAccountsForDatatableQuery);
            return Json(result);
        }
    }
}
