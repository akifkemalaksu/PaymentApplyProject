using System.ComponentModel;

namespace PaymentApplyProject.Application.Features.UserFeatures.LoadUsersForDatatable
{
    public class LoadUsersForDatatableResult
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Soyad")]
        public string Surname { get; set; }
        [DisplayName("Durum")]
        public bool Active { get; set; }
        [DisplayName("Firmalar")]
        public string Companies { get; set; }
        [DisplayName("Rol")]
        public string? Role { get; set; }
        [DisplayName("Ekleme Tarihi")]
        public DateTime AddDate { get; set; }
    }
}
