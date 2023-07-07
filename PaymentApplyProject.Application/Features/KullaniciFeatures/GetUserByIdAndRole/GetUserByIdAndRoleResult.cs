using PaymentApplyProject.Application.Dtos.KullaniciDtos;

namespace PaymentApplyProject.Application.Features.KullaniciFeatures.GetUserByIdAndRole
{
    public class GetUserByIdAndRoleResult
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Email { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool AktifMi { get; set; }
        public IEnumerable<YetkiDto> Yetkiler { get; set; }
        public IEnumerable<FirmaDto> Firmalar { get; set; }
    }
}
