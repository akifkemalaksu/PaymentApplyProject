using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken
{
    public class AuthenticateTokenCommand : IRequest<Response<AuthenticateTokenResult>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
