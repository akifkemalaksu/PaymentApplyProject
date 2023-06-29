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
    [Authorize(Roles = "user")]
    public class PaymentFrameController : CustomController
    {
        private readonly IMediator _mediator;

        public PaymentFrameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("[controller]/{musteriKey}")]
        public IActionResult Index(string musteriKey)
        {
            if (string.IsNullOrEmpty(musteriKey))
                return RedirectToAction("notfound","error",new { message = "Müşteri bulunamadı." });

            string musteriId = HttpContext.Session.GetString(musteriKey);
            // musteriId boş ise yönlendir
            ViewBag.MusteriId = musteriId;
            return View();
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
