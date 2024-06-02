using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.ChangeCustomerStatus
{
    public class ChangeCustomerStatusCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
    }
}
