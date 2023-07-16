using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services
{
    public interface INotificationService
    {
        public Task CreateNotification(CancellationToken cancellationToken = default, params object[]? args);
    }
}
