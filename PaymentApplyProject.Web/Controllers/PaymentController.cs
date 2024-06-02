using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Attributes;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.DepositFeatures.ApproveDeposit;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById;
using PaymentApplyProject.Application.Features.DepositFeatures.LoadDepositsForDatatable;
using PaymentApplyProject.Application.Features.DepositFeatures.RejectDeposit;
using PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw;
using PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById;
using PaymentApplyProject.Application.Features.WithdrawFeatures.LoadWithdrawsForDatatable;
using PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw;

namespace PaymentApplyProject.Web.Controllers
{
    [AdvancedAuthorize(Roles = "admin,user,accounting")]
    public class PaymentController : CustomController
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Deposits()
        {
            return View();
        }

        public IActionResult Withdraws()
        {
            return View();
        }

        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> ViewDepositPartial(int id)
        {
            var result = await _mediator.Send(new GetDepositByIdQuery { Id = id });
            return PartialView("_viewDepositPartial", result);
        }

        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> ViewWithdrawPartial(int id)
        {
            var result = await _mediator.Send(new GetWithdrawByIdQuery { Id = id });
            return PartialView("_viewWithdrawPartial", result);
        }

        [HttpPost]
        public async Task<IActionResult> LoadDeposits(LoadDepositsForDatatableQuery loadDepositsForDatatableQuery)
        {
            var result = await _mediator.Send(loadDepositsForDatatableQuery);
            return Json(result);
        }

        [AdvancedAuthorize(Roles = "admin,user")]
        [HttpPost]
        public async Task<IActionResult> ApproveDeposit([FromBody] ApproveDepositCommand approveParaYatirmaCommand)
        {
            var result = await _mediator.Send(approveParaYatirmaCommand);
            return CreateResult(result);
        }

        [AdvancedAuthorize(Roles = "admin,user")]
        [HttpPost]
        public async Task<IActionResult> RejectDeposit([FromBody] RejectDepositCommand rejectParaYatirmaCommand)
        {
            var result = await _mediator.Send(rejectParaYatirmaCommand);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoadWithdraws(LoadWithdrawsForDatatableQuery loadWithdrawsForDatatableQuery)
        {
            var result = await _mediator.Send(loadWithdrawsForDatatableQuery);
            return Json(result);
        }

        [AdvancedAuthorize(Roles = "admin,user")]
        [HttpPost]
        public async Task<IActionResult> ApproveWithdraw([FromBody] ApproveWithdrawCommand approveParaCekmeCommand)
        {
            var result = await _mediator.Send(approveParaCekmeCommand);
            return CreateResult(result);
        }

        [AdvancedAuthorize(Roles = "admin,user")]
        [HttpPost]
        public async Task<IActionResult> RejectWithdraw([FromBody] RejectWithdrawCommand rejectParaCekmeCommand)
        {
            var result = await _mediator.Send(rejectParaCekmeCommand);
            return CreateResult(result);
        }
    }
}
