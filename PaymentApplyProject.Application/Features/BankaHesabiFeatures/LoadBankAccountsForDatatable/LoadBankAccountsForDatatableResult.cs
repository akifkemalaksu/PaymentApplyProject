using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.LoadBankAccountsForDatatable
{
    public class LoadBankAccountsForDatatableResult
    {
        [DisplayName("Hesap Id")]
        public int Id { get; set; }
        public string Banka { get; set; }
        [DisplayName("Hesap Numarası")]
        public string HesapNumarasi { get; set; }
        [DisplayName("Hesap Sahibi")]
        public string AdSoyad { get; set; }
        [DisplayName("Üst Limit")]
        public decimal UstLimit { get; set; }
        [DisplayName("Alt Limit")]
        public decimal AltLimit { get; set; }
        [DisplayName("Durum")]
        public bool AktifMi { get; set; }
        [DisplayName("Ekleme Tarihi")]
        public DateTime EklemeTarihi { get; set; }
    }
}
