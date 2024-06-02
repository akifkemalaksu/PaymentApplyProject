using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Services.HubServices;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Infrastructure.Hubs;

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
{
    public class DepositPaymentHubRedirectionService : IDepositPaymentHubRedirectionService
    {
        private readonly IHubContext<DepositPaymentHub> _hubContext;
        private readonly IHubConnectionUniqueKeyCacheService _hubUserConnectionService;

        public DepositPaymentHubRedirectionService(IHubContext<DepositPaymentHub> hubContext, IHubConnectionUniqueKeyCacheService hubUserConnectionService)
        {
            _hubContext = hubContext;
            _hubUserConnectionService = hubUserConnectionService;
        }

        public Task Redirect(string redirectUrl, string hash, CancellationToken cancellationToken = default)
        {
            var connectionId = _hubUserConnectionService.GetConnection(hash);
            return _hubContext.Clients.Client(connectionId).SendAsync("redirect", redirectUrl, cancellationToken);
        }
    }
}
