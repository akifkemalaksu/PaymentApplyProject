using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.KullaniciFeatures.Authenticate;

namespace PaymentApplyProject.Web.Controllers
{
    public class LoginController : CustomController
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateCommand authenticateCommand)
        {
            var result = await _mediator.Send(authenticateCommand);
            return CreateResult(result);
        }
    }
}
