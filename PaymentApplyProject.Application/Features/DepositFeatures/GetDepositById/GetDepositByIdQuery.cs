using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById
{
    public class GetDepositByIdQuery:IRequest<Response<GetDepositByIdResult>>
    {
        public int Id { get; set; }
    }
}
