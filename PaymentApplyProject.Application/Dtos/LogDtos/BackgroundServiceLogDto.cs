﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    internal class BackgroundServiceLogDto: LogDto
    {
        public int ExecutionCount { get; set; }
    }
}
