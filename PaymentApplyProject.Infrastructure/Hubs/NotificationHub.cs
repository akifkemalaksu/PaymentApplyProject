using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Dtos.SignalRDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Hubs
{

    public class NotificationHub : BaseHub
    {
        public NotificationHub(IAuthenticatedUserService authenticatedUserService, IHubUserConnectionService hubUserConnectionService) : base(authenticatedUserService, hubUserConnectionService)
        {
        }
    }
}
