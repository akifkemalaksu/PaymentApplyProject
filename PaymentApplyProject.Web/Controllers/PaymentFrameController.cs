﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using System.Data;
using PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;

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

        [Route("[controller]/[action]/{musteriKey}")]
        public IActionResult Panel(string musteriKey)
        {
            if (string.IsNullOrEmpty(musteriKey))
                return RedirectToAction("notfound", "error", new { message = "Müşteri bulunamadı." });

            string musteriId = HttpContext.Session.GetString(musteriKey);

            if (string.IsNullOrEmpty(musteriId))
                return RedirectToAction("notfound", "error", new { message = "Müşteri bulunamadı." });

            ViewBag.MusteriId = musteriId;
            return View();
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
