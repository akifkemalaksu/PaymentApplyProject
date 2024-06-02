using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.DepositFeatures.LoadDepositsForDatatable
{
    public class LoadDepositsForDatatableResult
    {
        [DisplayName("Para Yatırma Id")]
        public int Id { get; set; }
        [DisplayName("Dış Kaynak Id")]
        public required string ExternalTransactionId { get; set; }
        [DisplayName("Firma")]
        public required string Company { get; set; }
        [DisplayName("Müşteri Kullanıcı Adı")]
        public required string CustomerUsername { get; set; }
        [DisplayName("Müşteri Ad Soyad")]
        public required string CustomerNameSurname { get; set; }
        public short StatusId { get; set; }
        [DisplayName("Durum")]
        public required string Status { get; set; }
        [DisplayName("Banka Hesap Sahibi")]
        public required string BankAccountOwner { get; set; }
        [DisplayName("Banka Hesap Numarası")]
        public required string BankAccountNumber { get; set; }
        [DisplayName("Banka")]
        public required string Bank { get; set; }
        [DisplayName("Tutar")]
        public decimal Amount { get; set; }
        [DisplayName("İşlem Tarihi")]
        public DateTime? TransactionDate { get; set; }
        [DisplayName("Talep Tarihi")]
        public DateTime AddDate { get; set; }
    }
}
