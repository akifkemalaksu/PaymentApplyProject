using Microsoft.AspNetCore.Components.RenderTree;
using PaymentApplyProject.Application.Dtos.SignalRDtos;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Services
{
    public class HubUserConnectionService : IHubUserConnectionService
    {
        private readonly ICacheService _cacheService;

        public HubUserConnectionService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void AddUserConnection(UserConnectionDto userConnection)
        {
            var userConnections = _cacheService.Get<List<UserConnectionDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            if (userConnections == null)
                userConnections = new List<UserConnectionDto>();
            userConnections.Add(userConnection);
            _cacheService.Set(CacheNameConstants.SIGNALR_CONNECTIONS, userConnections);
        }

        public List<UserConnectionDto> GetUserConnections()
        {
            var userConnections = _cacheService.Get<List<UserConnectionDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            if (userConnections == null) return new List<UserConnectionDto>();

            return userConnections;
        }

        public void RemoveUserConnection(string connectionId)
        {
            var userConnections = _cacheService.Get<List<UserConnectionDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            if (userConnections == null) return;

            var index = userConnections.FindIndex(x => x.ConnectionId == connectionId);
            if (index == -1) return;
            userConnections.RemoveAt(index);

            _cacheService.Set(CacheNameConstants.SIGNALR_CONNECTIONS, userConnections);
        }
    }
}
