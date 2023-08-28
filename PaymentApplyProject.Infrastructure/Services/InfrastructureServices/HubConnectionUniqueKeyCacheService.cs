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

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
{
    public class HubConnectionUniqueKeyCacheService : IHubConnectionUniqueKeyCacheService
    {
        private readonly ICacheService _cacheService;

        public HubConnectionUniqueKeyCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void AddUserConnection(ConnectionUniqueKeyDto  connectionUniqueKey)
        {
            var connections = _cacheService.Get<List<ConnectionUniqueKeyDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            if (connections == null)
                connections = new List<ConnectionUniqueKeyDto>();
            connections.Add(connectionUniqueKey);
            _cacheService.Set(CacheNameConstants.SIGNALR_CONNECTIONS, connections);
        }

        public List<ConnectionUniqueKeyDto> GetConnections()
        {
            var userConnections = _cacheService.Get<List<ConnectionUniqueKeyDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            if (userConnections == null) return new List<ConnectionUniqueKeyDto>();

            return userConnections;
        }

        public void RemoveConnection(string connectionId)
        {
            var connections = _cacheService.Get<List<ConnectionUniqueKeyDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            if (connections == null) return;

            var index = connections.FindIndex(x => x.ConnectionId == connectionId);
            if (index == -1) return;
            connections.RemoveAt(index);

            _cacheService.Set(CacheNameConstants.SIGNALR_CONNECTIONS, connections);
        }
    }
}
