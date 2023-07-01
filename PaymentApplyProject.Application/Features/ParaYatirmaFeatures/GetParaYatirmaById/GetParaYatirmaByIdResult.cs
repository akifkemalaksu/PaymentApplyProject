using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.GetParaYatirmaById
{
    public class GetParaYatirmaByIdResult
    {
        public int Id { get; set; }
        public string Firma { get; set; }
        public string MusteriKullaniciAd { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public short DurumId { get; set; }
        public string Durum { get; set; }
        public string BankaHesapSahipAd { get; set; }
        public string BankaHesapSahipSoyad { get; set; }
        public string BankaHesapNo { get; set; }
        public string Banka { get; set; }
        public decimal Tutar { get; set; }
        public decimal? OnaylananTutar { get; set; }
        public DateTime? IslemTarihi { get; set; }
        public DateTime EklemeTarihi { get; set; }
    }
}
