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

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.GetUserByIdAndRole
{
    public class GetUserByIdAndRoleQuery : IRequest<Response<GetUserByIdAndRoleResult>>
    {
        public int Id { get; set; }
        public int YetkiId { get; set; }
    }
}
