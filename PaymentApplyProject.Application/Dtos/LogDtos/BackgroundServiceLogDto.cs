using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    internal class BackgroundServiceLogDto
    {
        public string MachineName { get; } = Environment.MachineName;
        public string OSVersion { get; } = Environment.OSVersion.ToString();
        public int ExecutionCount { get; set; }
    }
}
