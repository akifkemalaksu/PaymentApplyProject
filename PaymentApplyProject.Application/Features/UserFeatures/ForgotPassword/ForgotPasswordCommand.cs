using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.UserFeatures.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<Response<NoContent>>
    {
        public string Email { get; set; }
    }
}
