using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole;
using PaymentApplyProject.Application.Features.UserFeatures.Logout;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Attributes
{
    public class AdvancedAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            CheckUser(context);
        }

        private void CheckUser(AuthorizationFilterContext context)
        {
            var authenticatedUserService = context.HttpContext.RequestServices.GetRequiredService<IAuthenticatedUserService>();
            var userId = authenticatedUserService.GetUserId();

            if (userId == 0) return;

            var userInfo = authenticatedUserService.GetUserInfo();
            if (userInfo.DoesHaveAdminRole()) return;

            var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();
            var userResult = mediator.Send(new GetUserByIdQuery { Id = userId }).GetAwaiter().GetResult();

            bool userCheck = userResult.Data != null && userResult.Data.Active;
            if (userCheck) return;

            mediator.Send(new LogoutCommand()).GetAwaiter().GetResult();

            context.Result = new RedirectResult("/Account/Login");
        }
    }
}
