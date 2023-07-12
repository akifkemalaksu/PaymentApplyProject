using MediatR;
using Microsoft.AspNetCore.Server.HttpSys;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.CustomerFeatures.AddOrUpdateAndGetCustomer;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.AddOrUpdateAndGetCustomer
{
    public class AddOrUpdateAndGetCustomerCommand : IRequest<Response<AddOrUpdateAndGetCustomerResult>>, ITransactional
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
    }
}
