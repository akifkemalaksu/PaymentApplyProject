using Mapster;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountById;
using PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame;
using PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForDatatable;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Infrastructure.Mapping.Mapster.Configs
{
    public class BankAccountConfigs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BankAccount, GetBankAccountByIdResult>()
                .Map(src => src.Bank, dest => dest.Bank.Name);

            config.NewConfig<BankAccount, GetBankAccountForPaymentFrameResult>()
                .Map(src => src.BankAccountId, dest => dest.Id);

            config.NewConfig<BankAccount, LoadBankAccountsForDatatableResult>()
                .Map(src => src.Bank, dest => dest.Bank.Name)
                .Map(src => src.NameSurname, dest => dest.Name + " " + dest.Surname);

            config.NewConfig<BankAccount, Option>()
                //.Map(src => src.Text, dest => $"{dest.Bank.Name} - {dest.Name} {dest.Surname} - {dest.AccountNumber}");
                .Map(src => src.Text, dest => dest.Bank.Name + " - " + dest.Name + " - " + dest.Surname + " - " + dest.AccountNumber);
        }
    }
}
