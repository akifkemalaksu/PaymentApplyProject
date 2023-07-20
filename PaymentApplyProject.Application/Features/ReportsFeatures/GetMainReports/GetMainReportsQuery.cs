﻿using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.ReportsFeatures.GetMainReports
{
    public class GetMainReportsQuery : IRequest<Response<GetMainReportsResult>>
    {
    }
}