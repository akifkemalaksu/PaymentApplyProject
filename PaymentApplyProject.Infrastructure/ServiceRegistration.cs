using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Infrastructure.Mapping;
using PaymentApplyProject.Infrastructure.Pipelines;
using PaymentApplyProject.Infrastructure.Services;
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
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            services.AddSingleton<ICustomMapper, MapstersMapper>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value);

            var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            services
                .AddAuthentication(auth =>
                {
                    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
                    };
                });

            return services;
        }
    }
}
