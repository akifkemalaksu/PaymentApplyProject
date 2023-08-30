using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Dtos.SignalRDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Infrastructure.Hubs;

namespace PaymentApplyProject.Infrastructure.Services.HubServices
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IHubConnectionUniqueKeyCacheService _hubConnectionUniqueKeyCacheService;


        public NotificationService(IHubContext<NotificationHub> hubContext, IHubConnectionUniqueKeyCacheService hubConnectionUniqueKeyCacheService)
        {
            _hubContext = hubContext;
            _hubConnectionUniqueKeyCacheService = hubConnectionUniqueKeyCacheService;
        }

        public Task CreateNotification(object data, CancellationToken cancellationToken = default) => _hubContext.Clients.All.SendAsync("displayNotification", data, cancellationToken);

        public Task CreateNotificationToSpecificUsers(IEnumerable<string> usernames, object data, CancellationToken cancellationToken = default)
        {
            var userConnections = _hubConnectionUniqueKeyCacheService.GetConnections();
            var connectionIds = userConnections.Where(x => usernames.Contains(x.UniqueKey)).Select(x => x.ConnectionId);
            return _hubContext.Clients.Clients(connectionIds).SendAsync("displayNotification", data, cancellationToken);
        }
    }
}
