using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Attributes;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.BankFeatures.GetBanks;
using PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;
using PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus;

namespace PaymentApplyProject.Web.Controllers
{
    [AdvancedAuthorize(Roles = "customer")]
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

        [HttpGet("GetWithdrawStatus/{transactionId}")]
        public async Task<IActionResult> GetWithdrawStatus(string transactionId)
        {
            var response = await _mediator.Send(new GetWithdrawStatusQuery { TransactionId = transactionId });
            return CreateResult(response);
        }

        [HttpPost("AddDeposit")]
        public async Task<IActionResult> Post(DepositRequestCommand depositRequestCommand)
        {
            var response = await _mediator.Send(depositRequestCommand);
            return CreateResult(response);
        }

        [HttpGet("GetDepositStatus/{transactionId}")]
        public async Task<IActionResult> GetDepositStatus(string transactionId)
        {
            var response = await _mediator.Send(new GetDepositStatusQuery { TransactionId = transactionId });
            return CreateResult(response);
        }

        [HttpGet("GetBanks")]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetBanksQuery());
            return CreateResult(response);
        }
    }
}
