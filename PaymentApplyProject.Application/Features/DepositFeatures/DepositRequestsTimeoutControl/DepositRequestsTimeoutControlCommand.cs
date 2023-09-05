using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.DepositFeatures.DepositRequestsTimeoutControl
{
    public class DepositRequestsTimeoutControlCommand : IRequest<Response<NoContent>>
    {
    }
}
