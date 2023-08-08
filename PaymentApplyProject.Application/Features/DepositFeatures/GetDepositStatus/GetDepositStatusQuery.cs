using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositStatus
{
    public class GetDepositStatusQuery : IRequest<Response<GetDepositStatusResult>>
    {
        public string TransactionId { get; set; }
    }
}
