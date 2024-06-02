using Mapster;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Infrastructure.Mapping.Mapster.Configs
{
    public class BankConfigs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Bank, Option>()
                .Map(dest => dest.Text, src => src.Name);
        }
    }
}
