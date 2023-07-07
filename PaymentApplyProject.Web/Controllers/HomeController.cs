using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Services;
using System.Data;
using System.Linq.Expressions;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
