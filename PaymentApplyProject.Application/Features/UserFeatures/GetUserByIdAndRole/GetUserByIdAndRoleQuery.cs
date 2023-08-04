﻿using MediatR;
using Microsoft.AspNetCore.Http.Features;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole
{
    public class GetUserByIdAndRoleQuery : IRequest<Response<GetUserByIdAndRoleResult>>
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
    }
}
