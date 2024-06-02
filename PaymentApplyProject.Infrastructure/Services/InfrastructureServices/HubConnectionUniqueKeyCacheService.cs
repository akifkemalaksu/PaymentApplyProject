using PaymentApplyProject.Application.Dtos.SignalRDtos;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;

namespace PaymentApplyProject.Infrastructure.Services.InfrastructureServices
{
    public class HubConnectionUniqueKeyCacheService : IHubConnectionUniqueKeyCacheService
    {
        private readonly ICacheService _cacheService;

        public HubConnectionUniqueKeyCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public void AddUserConnection(ConnectionUniqueKeyDto connectionUniqueKey)
        {
            var connections = _cacheService.Get<List<ConnectionUniqueKeyDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            connections ??= [];
            connections.Add(connectionUniqueKey);
            _cacheService.Set(CacheNameConstants.SIGNALR_CONNECTIONS, connections);
        }

        public List<ConnectionUniqueKeyDto> GetConnections()
        {
            var connections = _cacheService.Get<List<ConnectionUniqueKeyDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            return connections ?? ([]);
        }

        public string GetConnection(string uniqueKey)
        {
            var connections = _cacheService.Get<List<ConnectionUniqueKeyDto>>(CacheNameConstants.SIGNALR_CONNECTIONS);
            if (connections == null) return string.Empty;

            var connection = connections.FirstOrDefault(x => x.UniqueKey == uniqueKey);
            return connection == null ? string.Empty : connection.ConnectionId;
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
