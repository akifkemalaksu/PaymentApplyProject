using Microsoft.AspNetCore.Mvc;

namespace PaymentApplyProject.Web.Controllers
{
    public class ErrorController : Controller
    {
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
