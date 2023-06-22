using Microsoft.AspNetCore.Mvc;
using PaymentApplyProject.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Core.ControllerBases
{
    public class CustomApiControllerBase : ControllerBase
    {
        public IActionResult CreateResult<T>(Response<T> response) => new ObjectResult(response)
        {
            StatusCode = (int)response.StatusCode
        };
    }
}
