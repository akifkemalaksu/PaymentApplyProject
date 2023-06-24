using Microsoft.Extensions.DependencyInjection;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ICustomMapper, MapstersMapper>();

            return services;
        }
    }
}
