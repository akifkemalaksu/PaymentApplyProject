using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.DepositFeatures.LoadDepositsForDatatable
{
    public class LoadDepositsForDatatableResult
    {
        [DisplayName("Para Yatırma Id")]
        public int Id { get; set; }
        public string Company { get; set; }
        [DisplayName("Müsteri Kullanıcı Adı")]
        public string CustomerUsername { get; set; }
        [DisplayName("Müsteri Ad Soyad")]
        public string CustomerNameSurname { get; set; }
        public short StatusId { get; set; }
        [DisplayName("Durum")]
        public string Status { get; set; }
        [DisplayName("Banka Hesap Sahibi")]
        public string BankAccountOwner { get; set; }
        [DisplayName("Banka Hesap Numarası")]
        public string BankAccountNumber { get; set; }
        [DisplayName("Banka")]
        public string Bank { get; set; }
        public decimal Amount { get; set; }
        [DisplayName("Onaylanan Tutar")]
        public decimal? ApprovedAmount { get; set; }
        [DisplayName("İşlem Tarihi")]
        public DateTime? TransactionDate { get; set; }
        [DisplayName("Talep Tarihi")]
        public DateTime AddDate { get; set; }
    }
}
