namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankAccountById
{
    public class GetBankAccountByIdResult
    {
        public int Id { get; set; }
        public short BankaId { get; set; }
        public string Banka { get; set; }
        public string HesapNumarasi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public decimal UstLimit { get; set; }
        public decimal AltLimit { get; set; }
        public bool AktifMi { get; set; }
    }
}
