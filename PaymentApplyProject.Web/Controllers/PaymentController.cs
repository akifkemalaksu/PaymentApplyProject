using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Dtos;
using System.Data;
using System.Linq.Expressions;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.LoadWithdrawsForDatatable;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class PaymentController : Controller
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

        [HttpPost]
        public async Task<IActionResult> LoadDeposits(LoadDepositsForDatatableQuery loadDepositsForDatatableQuery)
        {
            var result = await _mediator.Send(loadDepositsForDatatableQuery);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoadWithdraws(LoadWithdrawsForDatatableQuery loadWithdrawsForDatatableQuery)
        {
            var result = await _mediator.Send(loadWithdrawsForDatatableQuery);
            return Json(result);
        }
    }
}
