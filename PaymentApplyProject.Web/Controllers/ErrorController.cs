using Microsoft.AspNetCore.Mvc;

namespace PaymentApplyProject.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
