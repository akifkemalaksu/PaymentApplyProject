using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.LoadUsersForDatatable
{
    public class LoadUsersForDatatableResult
    {
        [DisplayName("Kullanıcı Id")]
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [DisplayName("Durum")]
        public bool AktifMi { get; set; }
        public string Firmalar { get; set; }
        [DisplayName("Ekleme Tarihi")]
        public DateTime EklemeTarihi { get; set; }
    }
}
