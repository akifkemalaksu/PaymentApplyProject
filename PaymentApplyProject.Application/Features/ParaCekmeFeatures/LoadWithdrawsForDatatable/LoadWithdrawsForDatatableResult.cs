using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.LoadWithdrawsForDatatable
{
    public class LoadWithdrawsForDatatableResult
    {
        [DisplayName("Para Çekme Id")]
        public int Id { get; set; }
        public string Firma { get; set; }
        [DisplayName("Müsteri Kullanıcı Adı")]
        public string MusteriKullaniciAd { get; set; }
        [DisplayName("Müsteri Ad Soyad")]
        public string MusteriAdSoyad { get; set; }
        public short DurumId { get; set; }
        public string Durum { get; set; }
        [DisplayName("Hesap Numarası")]
        public string BankaHesapNo { get; set; }
        public decimal Tutar { get; set; }
        [DisplayName("Onaylanan Tutar")]
        public decimal? OnaylananTutar { get; set; }
        [DisplayName("İşlem Tarihi")]
        public DateTime? IslemTarihi { get; set; }
        [DisplayName("Talep Tarihi")]
        public DateTime TalepTarihi { get; set; }
    }
}
