using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }
    }
}
