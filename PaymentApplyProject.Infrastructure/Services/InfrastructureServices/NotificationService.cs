using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Dtos.SignalRDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Infrastructure.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IHubConnectionUniqueKeyCacheService _hubUserConnectionService;


        public NotificationService(IHubContext<NotificationHub> hubContext, IHubConnectionUniqueKeyCacheService hubUserConnectionService)
        {
            _hubContext = hubContext;
            _hubUserConnectionService = hubUserConnectionService;
        }

        public Task CreateNotification(object data, CancellationToken cancellationToken = default) => _hubContext.Clients.All.SendAsync("displayNotification", data, cancellationToken);

        public Task CreateNotificationToSpecificUsers(IEnumerable<string> usernames, object data, CancellationToken cancellationToken = default)
        {
            var userConnections = _hubUserConnectionService.GetConnections();
            var connectionIds = userConnections.Where(x => usernames.Contains(x.UniqueKey)).Select(x => x.ConnectionId);
            return _hubContext.Clients.Clients(connectionIds).SendAsync("displayNotification", data, cancellationToken);
        }
    }
}
