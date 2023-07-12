using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.UserFeatures.Login
{
    public class LoginCommand : IRequest<Response<NoContent>>
    {
        public string EmailUsername { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
