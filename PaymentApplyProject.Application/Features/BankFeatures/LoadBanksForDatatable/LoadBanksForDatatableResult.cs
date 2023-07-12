using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.BankFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableResult
    {
        [DisplayName("Banka Id")]
        public short Id { get; set; }
        [DisplayName("Banka")]
        public string Name { get; set; }
    }
}
