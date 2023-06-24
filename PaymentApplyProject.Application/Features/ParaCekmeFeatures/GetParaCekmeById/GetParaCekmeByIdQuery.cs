using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.GetParaCekmeById
{
    public class GetParaCekmeByIdQuery : IRequest<Response<GetParaCekmeByIdResult>>
    {
        public int Id { get; set; }
    }
}
