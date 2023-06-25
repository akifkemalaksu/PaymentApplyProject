namespace PaymentApplyProject.Application.Dtos.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public double TokenTimeoutHours { get; set; }
    }
}
