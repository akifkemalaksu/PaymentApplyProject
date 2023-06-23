using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma;
using PaymentApplyProject.Application.Features.ParaYatirmaFeatures.GetParaYatirmaById;
using PaymentApplyProject.Core.ControllerBases;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new GetParaYatirmaByIdQuery { Id = id });
            return CreateResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddParaYatirmaCommand addParaYatirmaCommand)
        {
            var response = await _mediator.Send(addParaYatirmaCommand);
            return CreateResult(response);
        }
    }
}
