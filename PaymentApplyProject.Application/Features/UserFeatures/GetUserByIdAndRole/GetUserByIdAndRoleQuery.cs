using MediatR;
using Microsoft.AspNetCore.Http.Features;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole;

namespace PaymentApplyProject.Application.Features.UserFeatures.GetUserByIdAndRole
{
    public class GetUserByIdAndRoleQuery : IRequest<Response<GetUserByIdAndRoleResult>>
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
    }
}
