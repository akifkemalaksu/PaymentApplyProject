using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.UserFeatures.ResetPasswordTokenCheck;

namespace PaymentApplyProject.Application.Features.UserFeatures.ResetPasswordTokenCheck
{
    public class ResetPasswordTokenCheckQuery : IRequest<Response<ResetPasswordTokenCheckResult>>
    {
        public string Token { get; set; }
    }
}
