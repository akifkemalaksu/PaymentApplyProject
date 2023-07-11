using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.FirmaFeatures.LoadCompaniesForDatatable
{
    public class LoadCompaniesForDatatableResult
    {
        [DisplayName("Firma Id")]
        public short Id { get; set; }
        [DisplayName("Firma")]
        public string Ad { get; set; }
        [DisplayName("Durum")]
        public bool AktifMi { get; set; }
    }
}
