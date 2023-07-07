using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable;
using PaymentApplyProject.Application.Features.KullaniciFeatures.AddUser;
using PaymentApplyProject.Application.Features.KullaniciFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Features.KullaniciFeatures.EditUser;
using PaymentApplyProject.Application.Features.KullaniciFeatures.GetUserByIdAndRole;
using PaymentApplyProject.Application.Features.KullaniciFeatures.LoadUsersForDatatable;
using PaymentApplyProject.Application.Features.KullaniciFeatures.Login;
using PaymentApplyProject.Application.Features.KullaniciFeatures.Logout;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class AccountController : CustomController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Users()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult ViewAddUserPartial()
        {
            return PartialView("_viewAddUserPartial");
        }

        [Route("[controller]/[action]/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ViewEditUserPartial(int id)
        {
            var result = await _mediator.Send(new GetUserByIdAndRoleQuery { Id = id, YetkiId = RolSabitler.USER_ID });
            return PartialView("_viewEditUserPartial", result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);
            return CreateResult(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateTokenCommand authenticateCommand)
        {
            var result = await _mediator.Send(authenticateCommand);
            return CreateResult(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> LoadUsers(LoadUsersForDatatableQuery loadUsersForDatatableQuery)
        {
            var result = await _mediator.Send(loadUsersForDatatableQuery);
            return Json(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserCommand addUserCommand)
        {
            var result = await _mediator.Send(addUserCommand);
            return CreateResult(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserCommand editUserCommand)
        {
            var result = await _mediator.Send(editUserCommand);
            return CreateResult(result);
        }
    }
}
