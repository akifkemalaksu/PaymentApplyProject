using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.LoadCustomersForDatatable
{
    public class LoadCustomersForDatatableResult
    {
        [DisplayName("Müşteri Id")]
        public int Id { get; set; }
        [DisplayName("Firma")]
        public string Company { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        [DisplayName("Ad Soyad")]
        public string NameSurname { get; set; }
        [DisplayName("Durum")]
        public bool Active { get; set; }
        [DisplayName("Ekleme Tarihi")]
        public DateTime AddDate { get; set; }
    }
}
