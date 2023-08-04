using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.DepositFeatures.ApproveDeposit
{
    public class ApproveDepositCommand : IRequest<Response<NoContent>>,ITransactional
    {
        public int Id { get; set; }
    }
}
