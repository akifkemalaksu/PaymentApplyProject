using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    internal class BackgroundServiceLogDto : LogDto
    {
        public string Name { get; set; }
        public int ExecutionCount { get; set; }
    }
}
