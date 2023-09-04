using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Dtos.SignalRDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services.HubServices
{
    public interface INotificationService
    {
        public Task CreateNotification(object data, CancellationToken cancellationToken = default);
        public Task CreateNotificationToSpecificUsers(IEnumerable<string> usernames, object data, CancellationToken cancellationToken = default);
    }
}
