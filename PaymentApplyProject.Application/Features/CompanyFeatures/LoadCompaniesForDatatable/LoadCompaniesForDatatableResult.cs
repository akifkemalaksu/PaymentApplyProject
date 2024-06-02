using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.LoadCompaniesForDatatable
{
    public class LoadCompaniesForDatatableResult
    {
        [DisplayName("Firma Id")]
        public short Id { get; set; }
        [DisplayName("Firma")]
        public required string Name { get; set; }
        [DisplayName("Durum")]
        public bool Active { get; set; }
    }
}
