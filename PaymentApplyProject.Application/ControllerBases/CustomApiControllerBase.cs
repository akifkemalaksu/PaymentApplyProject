using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
