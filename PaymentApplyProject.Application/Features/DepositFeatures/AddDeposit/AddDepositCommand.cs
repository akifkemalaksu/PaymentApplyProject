using MediatR;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;

namespace PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit
{
    public class AddDepositCommand : IRequest<Response<AddDepositResult>>, ITransactional
    {
        public int CustomerId { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; } 
    }
}
