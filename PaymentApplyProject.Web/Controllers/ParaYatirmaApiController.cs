using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.MusteriFeatures.AddMusteri;

namespace PaymentApplyProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParaYatirmaApiController : CustomApiControllerBase
    {
        private readonly IMediator _mediator;

        public ParaYatirmaApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AddOrUpdateAndGetMusteriCommand addOrUpdateAndGetMusteriCommand)
        {
            var response = await _mediator.Send(addOrUpdateAndGetMusteriCommand);
            if (!response.IsSuccessful)
                return CreateResult(response);

            HttpContext.Session.SetString("musteriId", response.Data.MusteriId.ToString());
            return RedirectToAction("index", "payments");
        }
    }
}
