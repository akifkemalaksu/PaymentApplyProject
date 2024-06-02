using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.UserFeatures.LoadUsersForDatatable
{
    public class LoadUsersForDatatableResult
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public required string Username { get; set; }
        [DisplayName("Email")]
        public required string Email { get; set; }
        [DisplayName("Ad")]
        public required string Name { get; set; }
        [DisplayName("Soyad")]
        public required string Surname { get; set; }
        [DisplayName("Durum")]
        public bool Active { get; set; }
        [DisplayName("Firmalar")]
        public required string Companies { get; set; }
        [DisplayName("Rol")]
        public string? Role { get; set; }
        [DisplayName("Ekleme Tarihi")]
        public DateTime AddDate { get; set; }
    }
}
