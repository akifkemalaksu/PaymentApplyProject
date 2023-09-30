using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.UserFeatures.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
