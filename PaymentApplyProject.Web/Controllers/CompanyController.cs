using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Attributes;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Features.CompanyFeatures.ChangeCompanyStatus;
using PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable;
using PaymentApplyProject.Application.Features.CustomerFeatures.ChangeCustomerStatus;
using PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable;

namespace PaymentApplyProject.Web.Controllers
{
    [AdvancedAuthorize(Roles = "admin,user,accounting")]
    public class CompanyController : CustomController
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AdvancedAuthorize(Roles = "admin,accounting")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customers()
        {
            return View();
        }

        [AdvancedAuthorize(Roles = "admin,accounting")]
        [HttpPost]
        public async Task<IActionResult> LoadCompanies(LoadCompaniesForDatatableQuery loadCompaniesForDatatableQuery)
        {
            var result = await _mediator.Send(loadCompaniesForDatatableQuery);
            return Json(result);
        }

        [AdvancedAuthorize(Roles = "admin")]
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

        [AdvancedAuthorize(Roles = "admin")]
        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> ChangeCustomerStatus(int id)
        {
            var result = await _mediator.Send(new ChangeCustomerStatusCommand { Id = id });
            return CreateResult(result);
        }
    }
}
