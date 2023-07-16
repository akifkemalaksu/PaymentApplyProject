using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Dtos.SignalRDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Infrastructure.Hubs
{
    public class BaseHub : Hub
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IHubUserConnectionService _hubUserConnectionService;
        public BaseHub(IAuthenticatedUserService authenticatedUserService, IHubUserConnectionService hubUserConnectionService)
        {
            _authenticatedUserService = authenticatedUserService;
            _hubUserConnectionService = hubUserConnectionService;
        }

        public override Task OnConnectedAsync()
        {
            _hubUserConnectionService.AddUserConnection(new()
            {
                ConnectionId = Context.ConnectionId,
                Username = _authenticatedUserService.GetUsername()
            });

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _hubUserConnectionService.RemoveUserConnection(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
