using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.Versioning;

namespace PaymentApplyProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("Info")]
        public IActionResult Info()
        {
            var info = new
            {
                OSVersion = Environment.OSVersion.ToString(),
                Environment.MachineName,
                EnvironmentVersion = Environment.Version,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                AspDotnetVersion = Assembly
                    .GetEntryAssembly()?
                    .GetCustomAttribute<TargetFrameworkAttribute>()?
                    .FrameworkName,
                SystemTime = DateTime.Now
            };

            return Ok(info);
        }
    }
}
