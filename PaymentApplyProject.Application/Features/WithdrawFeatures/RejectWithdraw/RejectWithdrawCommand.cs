using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw
{
    public class RejectWithdrawCommand : IRequest<Response<NoContent>>, ITransactional
    {
        public int Id { get; set; }
    }
}
