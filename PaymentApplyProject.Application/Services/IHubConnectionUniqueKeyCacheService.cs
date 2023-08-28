using PaymentApplyProject.Application.Dtos.SignalRDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services
{
    public interface IHubConnectionUniqueKeyCacheService
    {
        void AddUserConnection(ConnectionUniqueKeyDto userConnection);
        void RemoveConnection(string connectionId);
        List<ConnectionUniqueKeyDto> GetConnections();
    }
}
