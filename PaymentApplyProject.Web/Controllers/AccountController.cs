using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.ControllerBases;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Application.Features.UserFeatures.AddUser;
using PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Features.UserFeatures.EditUser;
using PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole;
using PaymentApplyProject.Application.Features.UserFeatures.LoadUsersForDatatable;
using PaymentApplyProject.Application.Features.UserFeatures.Login;
using PaymentApplyProject.Application.Features.UserFeatures.Logout;
using PaymentApplyProject.Application.Features.UserFeatures.DeleteUser;

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

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return RedirectToAction("Login");
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult ViewAddUserPartial()
        {
            return PartialView("_viewAddUserPartial");
        }

        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> ViewEditUserPartial(int id)
        {
            var result = await _mediator.Send(new GetUserByIdAndRoleQuery { Id = id, RoleId = RoleConstants.USER_ID });
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

        [HttpPost]
        public async Task<IActionResult> LoadUsers(LoadUsersForDatatableQuery loadUsersForDatatableQuery)
        {
            var result = await _mediator.Send(loadUsersForDatatableQuery);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserCommand addUserCommand)
        {
            var result = await _mediator.Send(addUserCommand);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserCommand editUserCommand)
        {
            var result = await _mediator.Send(editUserCommand);
            return CreateResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand deleteUserCommand)
        {
            var result = await _mediator.Send(deleteUserCommand);
            return CreateResult(result);
        }
    }
}
