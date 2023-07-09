using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.BankaFeatures.LoadBanksForDatatable
{
    public class LoadBanksForDatatableResult
    {
        [DisplayName("Banka Id")]
        public short Id { get; set; }
        [DisplayName("Banka")]
        public string Ad { get; set; }
    }
}
