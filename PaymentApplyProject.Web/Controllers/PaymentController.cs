using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Dtos;
using System.Data;
using System.Linq.Expressions;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.LoadWithdrawsForDatatable;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.ApproveParaYatirma;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.RejectParaYatirma;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.GetParaYatirmaById;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.GetParaCekmeById;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.ApproveParaCekme;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.RejectParaCekme;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin,user")]
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

        public IActionResult DepositsPartial()
        {
            return PartialView("Deposit");
        }

        public IActionResult Withdraws()
        {
            return View();
        }

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> ViewDepositPartial(int id)
        {
            var result = await _mediator.Send(new GetParaYatirmaByIdQuery { Id = id });
            return PartialView("_viewDepositPartial", result);
        }

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> ViewWithdrawPartial(int id)
        {
            var result = await _mediator.Send(new GetParaCekmeByIdQuery { Id = id });
            return PartialView("_viewWithdrawPartial", result);
        }

        [HttpPost]
        public async Task<IActionResult> LoadDeposits(LoadDepositsForDatatableQuery loadDepositsForDatatableQuery)
        {
            var result = await _mediator.Send(loadDepositsForDatatableQuery);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveDeposit([FromBody] ApproveParaYatirmaCommand approveParaYatirmaCommand)
        {
            var result = await _mediator.Send(approveParaYatirmaCommand);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RejectDeposit([FromBody] RejectParaYatirmaCommand rejectParaYatirmaCommand)
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

        [HttpPost]
        public async Task<IActionResult> ApproveWithdraw([FromBody] ApproveParaCekmeCommand approveParaCekmeCommand)
        {
            var result = await _mediator.Send(approveParaCekmeCommand);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RejectWithdraw([FromBody] RejectParaCekmeCommand rejectParaCekmeCommand)
        {
            var result = await _mediator.Send(rejectParaCekmeCommand);
            return CreateResult(result);
        }
    }
}
