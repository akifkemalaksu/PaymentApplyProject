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

namespace PaymentApplyProject.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IHubUserConnectionService _hubUserConnectionService;


        public NotificationService(IHubContext<NotificationHub> hubContext, IHubUserConnectionService hubUserConnectionService)
        {
            _hubContext = hubContext;
            _hubUserConnectionService = hubUserConnectionService;
        }

        public Task CreateNotification(object data, CancellationToken cancellationToken = default) => _hubContext.Clients.All.SendAsync("displayNotification", data, cancellationToken);

        public Task CreateNotificationToSpecificUsers(IEnumerable<string> usernames, object data, CancellationToken cancellationToken = default)
        {
            var userConnections = _hubUserConnectionService.GetUserConnections();
            var connectionIds = userConnections.Where(x => usernames.Contains(x.Username)).Select(x => x.ConnectionId);
            return _hubContext.Clients.Clients(connectionIds).SendAsync("displayNotification", data, cancellationToken);
        }
    }
}
