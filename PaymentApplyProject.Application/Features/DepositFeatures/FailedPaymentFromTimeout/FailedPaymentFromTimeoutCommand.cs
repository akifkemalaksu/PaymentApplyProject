using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.DepositFeatures.FailedPaymentFromTimeout
{
    public class FailedPaymentFromTimeoutCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
    }
}
