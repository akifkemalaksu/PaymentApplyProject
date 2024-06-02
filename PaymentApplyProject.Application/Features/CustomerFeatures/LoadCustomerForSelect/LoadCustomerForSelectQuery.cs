using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomerForSelect
{
    public class LoadCustomerForSelectQuery : SelectParameters, IRequest<SelectResult>
    {
        public int CompanyId { get; set; }
    }
}
