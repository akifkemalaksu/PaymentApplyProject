namespace PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken
{
    public class AuthenticateTokenResult
    {
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
