namespace PaymentApplyProject.Application.Services.HubServices
{
    public interface IDepositPaymentHubRedirectionService
    {
        Task Redirect(string redirectUrl, string hash, CancellationToken cancellationToken = default);
    }
}
