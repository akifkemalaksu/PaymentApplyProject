using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.SignalRDtos
{
    public class ConnectionUniqueKeyDto
    {
        public string UniqueKey { get; set; }
        public string ConnectionId { get; set; }
    }
}
