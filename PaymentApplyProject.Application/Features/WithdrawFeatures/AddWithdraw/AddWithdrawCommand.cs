using MediatR;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw
{
    public class AddWithdrawCommand : IRequest<Response<AddWithdrawResult>>, ITransactional
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public int IntegrationId { get; set; }
    }
}
