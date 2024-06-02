using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Attributes;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.BankAccountFeatures.AddBankAccount;
using PaymentApplyProject.Application.Features.BankAccountFeatures.DeleteBankAccount;
using PaymentApplyProject.Application.Features.BankAccountFeatures.EditBankAccount;
using PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountById;
using PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForDatatable;
using PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForDatatable;

namespace PaymentApplyProject.Web.Controllers
{
    [AdvancedAuthorize(Roles = "admin,user,accounting")]
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

        [AdvancedAuthorize(Roles = "admin")]
        public IActionResult ViewAddBankAccountPartial()
        {
            return PartialView("_viewAddBankAccountPartial");
        }

        [AdvancedAuthorize(Roles = "admin")]
        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> ViewEditBankAccountPartial(int id)
        {
            var result = await _mediator.Send(new GetBankAccountByIdQuery { Id = id });
            return PartialView("_viewEditBankAccountPartial", result);
        }


        [HttpPost]
        public async Task<IActionResult> LoadBanks(LoadBanksForDatatableQuery loadBanksForDatatableQuery)
        {
            var result = await _mediator.Send(loadBanksForDatatableQuery);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoadBankAccounts(LoadBankAccountsForDatatableQuery loadBankAccountsForDatatableQuery)
        {
            var result = await _mediator.Send(loadBankAccountsForDatatableQuery);
            return Json(result);
        }

        [AdvancedAuthorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddBankAccount(AddBankAccountCommand addBankAccountCommand)
        {
            var result = await _mediator.Send(addBankAccountCommand);
            return CreateResult(result);
        }

        [AdvancedAuthorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditBankAccount(EditBankAccountCommand editBankAccountCommand)
        {
            var result = await _mediator.Send(editBankAccountCommand);
            return CreateResult(result);
        }

        [AdvancedAuthorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteBankAccount([FromBody] DeleteBankAccountCommand deleteBankAccountCommand)
        {
            var result = await _mediator.Send(deleteBankAccountCommand);
            return CreateResult(result);
        }
    }
}
