using PaymentApplyProject.Application.Dtos.SignalRDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Services
{
    public interface IHubUserConnectionService
    {
        void AddUserConnection(UserConnectionDto userConnection);
        void RemoveUserConnection(string connectionId);
        List<UserConnectionDto> GetUserConnections();
    }
}
