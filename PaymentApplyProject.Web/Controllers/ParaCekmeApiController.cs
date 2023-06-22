﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.GetParaCekmeById;
using PaymentApplyProject.Core.ControllerBases;

namespace PaymentApplyProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParaCekmeApiController : CustomApiControllerBase
    {
        private readonly IMediator _mediator;

        public ParaCekmeApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetParaCekmeByIdQuery { Id = id });
            return CreateResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var response = await _mediator.Send(new GetParaCekmeByIdQuery());
            return CreateResult(response);
        }
    }
}
