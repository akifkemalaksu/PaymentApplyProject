using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.ReportsFeatures.GetMainReports;
using PaymentApplyProject.Application.Services;
using System.Data;
using System.Linq.Expressions;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetMainReportsQuery());
            return View(result);
        }
    }
}
