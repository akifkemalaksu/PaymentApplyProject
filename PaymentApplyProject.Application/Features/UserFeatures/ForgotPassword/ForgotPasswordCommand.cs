﻿using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<Response<NoContent>>
    {
        public string Email { get; set; }
    }
}
