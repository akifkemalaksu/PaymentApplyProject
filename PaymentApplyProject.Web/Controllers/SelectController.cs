using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Features.BankaFeatures.LoadBankalarForSelect;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankaHesaplarForSelect;
using PaymentApplyProject.Application.Features.FirmaFeatures.LoadFirmalarForSelect;
using PaymentApplyProject.Application.Features.MusteriFeatures.LoadMusterilerForSelect;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class SelectController : Controller
    {
        private readonly IMediator _mediator;

        public SelectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache")]
        public async Task<IActionResult> Firmalar(LoadFirmalarForSelectQuery loadFirmalarForSelectQuery)
        {
            var result = await _mediator.Send(loadFirmalarForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache", VaryByQueryKeys = new string[] { "FirmaId" })]
        public async Task<IActionResult> Musteriler(LoadMusterilerForSelectQuery loadMusterilerForSelectQuery)
        {
            var result = await _mediator.Send(loadMusterilerForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache")]
        public async Task<IActionResult> Bankalar(LoadBankalarForSelectQuery loadBankalarForSelectQuery)
        {
            var result = await _mediator.Send(loadBankalarForSelectQuery);
            return Json(result);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "SelectCache", VaryByQueryKeys = new string[] { "BankaId" })]
        public async Task<IActionResult> BankaHesaplar(LoadBankaHesaplarForSelectQuery loadBankaHesaplarForSelectQuery)
        {
            var result = await _mediator.Send(loadBankaHesaplarForSelectQuery);
            return Json(result);
        }
    }
}
