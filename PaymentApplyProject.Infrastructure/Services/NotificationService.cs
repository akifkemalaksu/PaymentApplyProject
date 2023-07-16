using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Services;
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

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task CreateNotification(CancellationToken cancellationToken = default, params object[]? args) => _hubContext.Clients.All.SendCoreAsync("displayNotification", args, cancellationToken);
    }
}
