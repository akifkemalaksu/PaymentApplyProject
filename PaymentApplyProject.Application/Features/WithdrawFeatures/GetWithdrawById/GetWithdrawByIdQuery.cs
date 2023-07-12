using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.GetWithdrawById
{
    public class GetWithdrawByIdQuery : IRequest<Response<GetWithdrawByIdResult>>
    {
        public int Id { get; set; }
    }
}
