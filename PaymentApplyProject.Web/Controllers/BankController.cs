using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.AddBankAccount;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.DeleteBankAccount;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankAccountById;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable;
using System.Data;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class BankController : CustomController
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

        public IActionResult ViewAddBankAccountPartial()
        {
            return PartialView("_viewAddBankAccountPartial");
        }

        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> ViewEditBankAccountPartial(int id)
        {
            var result = await _mediator.Send(new GetBankAccountByIdQuery { Id = id });
            return PartialView("_viewEditBankAccountPartial", result);
        }

        public async Task<IActionResult> LoadBankAccounts(LoadBankAccountsForDatatableQuery loadBankAccountsForDatatableQuery)
        {
            var result = await _mediator.Send(loadBankAccountsForDatatableQuery);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddBankAccount(AddBankAccountCommand addBankAccountCommand)
        {
            var result = await _mediator.Send(addBankAccountCommand);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBankAccount([FromBody] DeleteBankAccountCommand deleteBankAccountCommand)
        {
            var result = await _mediator.Send(deleteBankAccountCommand);
            return CreateResult(result);
        }
    }
}
