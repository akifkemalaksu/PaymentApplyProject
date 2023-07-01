﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using System;
using PaymentApplyProject.Application.Features.MusteriFeatures.AddOrUpdateAndGetMusteri;

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

        [HttpPost]
        /**
         * [FromQuery] tarayıcı da denemek için, normalde ise json olarak göndermesi gerekli
         * tarayıcı da denemek için alttaki linkten gidilebilir
         * http://localhost:6020/api/parayatirmaapi?FirmaKodu=GrandPasha&MusteriAd=Akif%20Kemal&MusteriSoyad=Aksu&MusteriKullaniciAdi=akifkemalaksu
         */
        public async Task<IActionResult> Get(AddOrUpdateAndGetMusteriCommand addOrUpdateAndGetMusteriCommand)
        {
            var response = await _mediator.Send(addOrUpdateAndGetMusteriCommand);
            if (!response.IsSuccessful)
                return CreateResult(response);

            string musteriKey = Guid.NewGuid().ToString();
            HttpContext.Session.SetString(musteriKey, response.Data.MusteriId.ToString());
            return RedirectToAction("index", "PaymentFrame", new { musteriKey });
        }
    }
}
