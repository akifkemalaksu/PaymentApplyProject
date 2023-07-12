using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById;

namespace PaymentApplyProject.Application.Features.DepositFeatures.GetDepositById
{
    public class GetDepositByIdQuery:IRequest<Response<GetDepositByIdResult>>
    {
        public int Id { get; set; }
    }
}
