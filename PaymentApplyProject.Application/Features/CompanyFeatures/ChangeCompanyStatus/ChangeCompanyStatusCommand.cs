using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.ChangeCompanyStatus
{
    public class ChangeCompanyStatusCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
    }
}
