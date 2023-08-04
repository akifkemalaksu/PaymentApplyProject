using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Features.DepositFeatures.DepositRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositRequestFromHash
{
    public class GetDepositRequestFromHashQuery : IRequest<Response<GetDepositRequestFromHashResult>>
    {
        public string HashKey { get; set; }
    }
}
