﻿using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForDatatable
{
    public class LoadBankAccountsForDatatableResult
    {
        [DisplayName("Hesap Id")]
        public int Id { get; set; }
        [DisplayName("Banka")]
        public required string Bank { get; set; }
        [DisplayName("Hesap Numarası")]
        public required string AccountNumber { get; set; }
        [DisplayName("Hesap Sahibi")]
        public required string NameSurname { get; set; }
        [DisplayName("Üst Limit")]
        public decimal UpperLimit { get; set; }
        [DisplayName("Alt Limit")]
        public decimal LowerLimit { get; set; }
        [DisplayName("Durum")]
        public bool Active { get; set; }
        [DisplayName("Ekleme Tarihi")]
        public DateTime AddDate { get; set; }
    }
}
