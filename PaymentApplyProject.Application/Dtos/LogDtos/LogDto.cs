using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public abstract class LogDto
    {
        public string MachineName { get; } = Environment.MachineName;
        public string OSVersion { get; } = Environment.OSVersion.ToString();
    }
}
