using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using PaymentApplyProject.Application.Features.CustomerFeatures.AddOrUpdateAndGetCustomer;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentApiController : CustomApiControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddWithdraw")]
        public async Task<IActionResult> Post(AddWithdrawCommand addParaCekmeCommand)
        {
            var response = await _mediator.Send(addParaCekmeCommand);
            return CreateResult(response);
        }

        [HttpPost("AddDeposit")]
        /**
         * [FromQuery] tarayıcı da denemek için, normalde ise json olarak göndermesi gerekli
         * tarayıcı da denemek için alttaki linkten gidilebilir
         * http://localhost:6020/api/parayatirmaapi?FirmaKodu=GrandPasha&MusteriAd=Akif%20Kemal&MusteriSoyad=Aksu&MusteriKullaniciAdi=akifkemalaksu
         */
        public async Task<IActionResult> Post(AddOrUpdateAndGetCustomerCommand addOrUpdateAndGetMusteriCommand)
        {
            var response = await _mediator.Send(addOrUpdateAndGetMusteriCommand);
            if (!response.IsSuccessful)
                return CreateResult(response);

            string musteriKey = Guid.NewGuid().ToString();
            HttpContext.Session.SetString(musteriKey, response.Data.CustomerId.ToString());
            return RedirectToAction("Panel", "PaymentFrame", new { musteriKey });
        }
    }
}
