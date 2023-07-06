﻿using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.LoadDepositsForDatatable
{
    public class LoadDepositsForDatatableResult
    {
        [DisplayName("Para Yatırma Id")]
        public int Id { get; set; }
        [DisplayName("Firma")]
        public string Firma { get; set; }
        [DisplayName("Müsteri Kullanıcı Adı")]
        public string MusteriKullaniciAd { get; set; }
        [DisplayName("Müsteri Ad Soyad")]
        public string MusteriAdSoyad { get; set; }
        public short DurumId { get; set; }
        [DisplayName("Durum")]
        public string Durum { get; set; }
        [DisplayName("Banka Hesap Sahibi")]
        public string BankaHesapSahibi { get; set; }
        [DisplayName("Banka Hesap Numarası")]
        public string BankaHesapNo { get; set; }
        [DisplayName("Banka")]
        public string Banka { get; set; }
        [DisplayName("Tutar")]
        public decimal Tutar { get; set; }
        [DisplayName("Onaylanan Tutar")]
        public decimal? OnaylananTutar { get; set; }
        [DisplayName("İşlem Tarihi")]
        public DateTime? IslemTarihi { get; set; }
        [DisplayName("Talep Tarihi")]
        public DateTime TalepTarihi { get; set; }
    }
}
