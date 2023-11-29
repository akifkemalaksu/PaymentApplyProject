﻿using MediatR;
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
using PaymentApplyProject.Application.Features.UserFeatures.ForgotPassword;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Features.UserFeatures.ResetPasswordTokenCheck;
using PaymentApplyProject.Application.Features.UserFeatures.ResetPassword;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin,user,accounting")]
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

        [Authorize(Roles = "admin,accounting")]
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
        public async Task<IActionResult> Forgot(ForgotPasswordCommand forgotPasswordCommand)
        {
            var result = await _mediator.Send(forgotPasswordCommand);
            return CreateResult(result);
        }

        [Route("[controller]/[action]/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string token)
        {
            var result = await _mediator.Send(new ResetPasswordTokenCheckQuery { Token = token });
            if (!result.IsSuccessful)
                return RedirectToAction("login", "account", new { message = result.Message });

            return View(result.Data);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPasswordProcess(ResetPasswordCommand resetPasswordCommand)
        {
            var result = await _mediator.Send(resetPasswordCommand);
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserCommand deleteUserCommand)
        {
            var result = await _mediator.Send(deleteUserCommand);
            return CreateResult(result);
        }
    }
}
