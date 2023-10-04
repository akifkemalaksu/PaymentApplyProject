using MediatR;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit
{
    public class AddDepositCommand : IRequest<Response<AddDepositResult>>, ITransactional
    {
        public int DepositRequestId { get; set; }
        public int CustomerId { get; set; }
        public int BankAccountId { get; set; }
    }
}
