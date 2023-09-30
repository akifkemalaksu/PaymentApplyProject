using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.WithdrawFeatures.LoadWithdrawsForDatatable
{
    public class LoadWithdrawsForDatatableResult
    {
        [DisplayName("Para Çekme Id")]
        public int Id { get; set; }
        [DisplayName("Dış Kaynak Id")]
        public string ExternalTransactionId { get; set; }
        [DisplayName("Firma")]
        public string Company { get; set; }
        [DisplayName("Müşteri Kullanıcı Adı")]
        public string Username { get; set; }
        [DisplayName("Müşteri Ad Soyad")]
        public string NameSurname { get; set; }
        public short StatusId { get; set; }
        [DisplayName("Durum")]
        public string Status { get; set; }
        [DisplayName("Hesap Numarası")]
        public string AccountNumber { get; set; }
        [DisplayName("Tutar")]
        public decimal Amount { get; set; }

        [DisplayName("İşlem Tarihi")]
        public DateTime? TransactionDate { get; set; }
        [DisplayName("Talep Tarihi")]
        public DateTime AddDate { get; set; }
    }
}
