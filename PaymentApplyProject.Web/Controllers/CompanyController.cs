using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.FirmaFeatures.ChangeCompanyStatus;
using PaymentApplyProject.Application.Features.FirmaFeatures.LoadCompaniesForDatatable;
using PaymentApplyProject.Application.Features.MusteriFeatures.ChangeCustomerStatus;
using PaymentApplyProject.Application.Features.MusteriFeatures.LoadCustomersForDatatable;

namespace PaymentApplyProject.Web.Controllers
{
    public class CompanyController : CustomController
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customers()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadCompanies(LoadCompaniesForDatatableQuery loadCompaniesForDatatableQuery)
        {
            var result = await _mediator.Send(loadCompaniesForDatatableQuery);
            return Json(result);
        }

        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> ChangeCompanyStatus(int id)
        {
            var result = await _mediator.Send(new ChangeCompanyStatusCommand { Id = id });
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> LoadCustomers(LoadCustomersForDatatableQuery loadCustomersForDatatableQuery)
        {
            var result = await _mediator.Send(loadCustomersForDatatableQuery);
            return Json(result);
        }

        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> ChangeCustomerStatus(int id)
        {
            var result = await _mediator.Send(new ChangeCustomerStatusCommand { Id = id });
            return CreateResult(result);
        }
    }
}
