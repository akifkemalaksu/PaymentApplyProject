using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using System.Data;
using PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "customer")]
    public class PaymentFrameController : CustomController
    {
        private readonly IMediator _mediator;

        public PaymentFrameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("[controller]/[action]/{key}")]
        public async Task<IActionResult> Panel(string key)
        {
            if (string.IsNullOrEmpty(key))
                return RedirectToAction("notfound", "error", new { message = $"Error code: {ErrorCodes.KeyValueIsNull}" });

            var depositRequest = await _mediator.Send(new GetDepositRequestFromHashQuery { HashKey = key });

            if (!depositRequest.IsSuccessful)
                return RedirectToAction("index", "error", new { message = $"Error code: {depositRequest.ErrorCode}", statusCode = depositRequest.StatusCode });

            return View(depositRequest.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GetAccountInfo([FromBody] GetBankAccountForPaymentFrameQuery getBankaHesabiQuery)
        {
            var result = await _mediator.Send(getBankaHesabiQuery);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> SavePayment([FromBody] AddDepositCommand addParaYatirmaCommand)
        {
            var result = await _mediator.Send(addParaYatirmaCommand);
            return CreateResult(result);
        }
    }
}
