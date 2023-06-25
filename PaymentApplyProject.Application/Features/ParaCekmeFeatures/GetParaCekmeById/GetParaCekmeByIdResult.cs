﻿namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.GetParaCekmeById
{
    public class GetParaCekmeByIdResult
    {
        public string Firma { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriKullaniciAdi { get; set; }
        public string Durum { get; set; }
        public string HesapNumarasi { get; set; }
        public decimal Tutar { get; set; }
        public decimal? OnaylananTutar { get; set; }
    }
}
