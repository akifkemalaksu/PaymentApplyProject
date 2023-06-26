﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(conf =>
                conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}