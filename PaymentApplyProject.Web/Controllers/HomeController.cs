using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Attributes;
using PaymentApplyProject.Application.Features.ReportsFeatures.GetMainReports;

namespace PaymentApplyProject.Web.Controllers
{
    [AdvancedAuthorize(Roles = "admin,user,accounting")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PaymentReport()
        {
            var result = await _mediator.Send(new GetMainReportsQuery());
            return PartialView("_paymentsReport", result);
        }
    }
}
