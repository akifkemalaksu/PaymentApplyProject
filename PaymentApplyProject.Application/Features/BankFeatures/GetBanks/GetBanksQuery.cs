using MediatR;
using PaymentApplyProject.Application.Dtos.BankDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.BankFeatures.GetBanks
{
    public class GetBanksQuery : IRequest<Response<IEnumerable<BankDto>>>
    {
    }
}
