using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableResult
    {
        [DisplayName("Banka Id")]
        public short Id { get; set; }
        [DisplayName("Banka")]
        public required string Name { get; set; }
    }
}
