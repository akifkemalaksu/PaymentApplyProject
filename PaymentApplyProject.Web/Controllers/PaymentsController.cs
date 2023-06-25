using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankaHesabi;
using PaymentApplyProject.Application.Features.MusteriFeatures.AddMusteri;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma;
using System.Data;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class PaymentsController : CustomController
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index(AddOrUpdateAndGetMusteriResult addOrUpdateAndGetMusteriResult)
        {
            string musteriId = HttpContext.Session.GetString("musteriId");
            // musteriId boş ise yönlendir
            ViewBag.MusteriId = musteriId;
            return View(addOrUpdateAndGetMusteriResult);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccountInfo([FromBody] GetBankaHesabiQuery getBankaHesabiQuery)
        {
            var result = await _mediator.Send(getBankaHesabiQuery);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> SavePayment([FromBody] AddParaYatirmaCommand addParaYatirmaCommand)
        {
            var result = await _mediator.Send(addParaYatirmaCommand);
            return CreateResult(result);
        }
    }
}
