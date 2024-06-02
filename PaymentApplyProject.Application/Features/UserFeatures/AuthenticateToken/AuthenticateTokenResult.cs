namespace PaymentApplyProject.Application.Features.UserFeatures.AuthenticateToken
{
    public class AuthenticateTokenResult
    {
        public required string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
