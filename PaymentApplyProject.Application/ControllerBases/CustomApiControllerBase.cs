using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.ControllerBases
{
    public class CustomApiControllerBase : ControllerBase
    {
        public IActionResult CreateResult<T>(Response<T> response) => new ObjectResult(response)
        {
            StatusCode = (int)response.StatusCode
        };
    }
}
