using PaymentApplyProject.Application.Dtos.SignalRDtos;

namespace PaymentApplyProject.Application.Services.InfrastructureServices
{
    public interface IHubConnectionUniqueKeyCacheService
    {
        void AddUserConnection(ConnectionUniqueKeyDto userConnection);
        void RemoveConnection(string connectionId);
        List<ConnectionUniqueKeyDto> GetConnections();
        string GetConnection(string uniqueKey);
    }
}
