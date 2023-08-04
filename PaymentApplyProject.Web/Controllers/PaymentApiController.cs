using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest;

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
        public async Task<IActionResult> Post(DepositRequestCommand depositRequestCommand)
        {
            var response = await _mediator.Send(depositRequestCommand);
            return CreateResult(response);
        }
    }
}
