using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.LoadCustomersForDatatable
{
    public class LoadCustomersForDatatableResult
    {
        [DisplayName("Müşteri Id")]
        public int Id { get; set; }
        public string Firma { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        [DisplayName("Ad Soyad")]
        public string AdSoyad { get; set; }
        [DisplayName("Durum")]
        public bool AktifMi { get; set; }
        public DateTime EklemeTarihi { get; set; }
    }
}
