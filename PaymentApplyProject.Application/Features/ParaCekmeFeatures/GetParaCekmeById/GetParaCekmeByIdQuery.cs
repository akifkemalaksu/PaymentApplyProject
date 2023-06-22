using MediatR;
using PaymentApplyProject.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.GetParaCekmeById
{
    public class GetParaCekmeByIdQuery : IRequest<Response<GetParaCekmeByIdResult>>
    {
        public int Id { get; set; }
    }
}
