using Mapster;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Infrastructure.Mapping.Mapster.Configs
{
    public class CustomerConfigs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, Option>()
                .Map(dest => dest.Text, src => src.Name + " " + src.Surname);

            config.NewConfig<Customer, LoadCustomersForDatatableResult>()
                .Map(dest => dest.NameSurname, src => src.Name + " " + src.Surname)
                .Map(dest => dest.Company, src => src.Company.Name);
        }
    }
}
