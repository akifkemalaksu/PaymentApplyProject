using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.GetParaCekmeById;
using PaymentApplyProject.Application.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParaCekmeApiController : CustomApiControllerBase
    {
        private readonly IMediator _mediator;

        public ParaCekmeApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddParaCekmeCommand addParaCekmeCommand)
        {
            var response = await _mediator.Send(addParaCekmeCommand);
            return CreateResult(response);
        }
    }
}
