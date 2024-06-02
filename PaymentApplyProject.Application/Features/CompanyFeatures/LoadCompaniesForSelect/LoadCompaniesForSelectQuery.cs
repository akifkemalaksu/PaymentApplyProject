using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForSelect
{
    public class LoadCompaniesForSelectQuery : SelectParameters, IRequest<SelectResult>
    {
    }
}
