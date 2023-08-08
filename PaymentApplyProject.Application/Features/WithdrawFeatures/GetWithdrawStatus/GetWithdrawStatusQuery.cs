using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Features.WithdrawFeatures.AddWithdraw;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawStatus
{
    public class GetWithdrawStatusQuery : IRequest<Response<GetWithdrawStatusResult>>
    {
        public string TransactionId { get; set; }
    }
}
