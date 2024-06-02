using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Services.InfrastructureServices;

namespace PaymentApplyProject.Infrastructure.Hubs
{
    public class DepositPaymentHub : Hub
    {
        private readonly IHubConnectionUniqueKeyCacheService _hubUserConnectionService;
        public DepositPaymentHub(IHubConnectionUniqueKeyCacheService hubUserConnectionService)
        {
            _hubUserConnectionService = hubUserConnectionService;
        }

        public void Subscribe(string hash)
        {
            _hubUserConnectionService.AddUserConnection(new()
            {
                ConnectionId = Context.ConnectionId,
                UniqueKey = hash
            });
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _hubUserConnectionService.RemoveConnection(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
