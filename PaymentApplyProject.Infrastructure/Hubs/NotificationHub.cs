﻿using Microsoft.AspNetCore.SignalR;
using PaymentApplyProject.Application.Dtos.SignalRDtos;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Hubs
{

    public class NotificationHub : Hub
    {
        private readonly IAuthenticatedUserService _authenticatedUserService;
        private readonly IHubConnectionUniqueKeyCacheService _hubUserConnectionService;
        public NotificationHub(IAuthenticatedUserService authenticatedUserService, IHubConnectionUniqueKeyCacheService hubUserConnectionService)
        {
            _authenticatedUserService = authenticatedUserService;
            _hubUserConnectionService = hubUserConnectionService;
        }

        public override Task OnConnectedAsync()
        {
            _hubUserConnectionService.AddUserConnection(new()
            {
                ConnectionId = Context.ConnectionId,
                UniqueKey = _authenticatedUserService.GetUsername()
            });

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _hubUserConnectionService.RemoveConnection(Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
