using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace PaymentApplyProject.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class BankController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
