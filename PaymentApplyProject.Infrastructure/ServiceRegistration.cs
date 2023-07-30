using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Services;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Infrastructure.Mapping;
using PaymentApplyProject.Infrastructure.Services.InfrastructureServices;
using PaymentApplyProject.Infrastructure.Services.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Pipelines;
using Serilog;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Middlewares;

namespace PaymentApplyProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssembly(typeof(IPaymentContext).Assembly);
                conf.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            });

            services.AddSingleton<ICustomMapper, MapstersMapper>();

            services.AddSingleton<IJwtAuthService, JwtAuthService>();
            services.AddSingleton<ICookieAuthService, CookieAuthService>();
            services.AddSingleton<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<ICacheService, InMemoryCacheService>();
            services.AddSingleton<IHubUserConnectionService, HubUserConnectionService>();

            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value);

            var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            services
                .AddAuthentication(auth =>
                {
                    auth.DefaultScheme = AuthenticationConstants.CustomAuthenticationScheme;
                    auth.DefaultAuthenticateScheme = AuthenticationConstants.CustomAuthenticationScheme;
                    auth.DefaultChallengeScheme = AuthenticationConstants.CustomAuthenticationScheme;
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
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login";
                    options.LogoutPath = "/account/logout";
                    options.AccessDeniedPath = "/error/accessdenied";
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.SlidingExpiration = true;
                })
                .AddPolicyScheme(AuthenticationConstants.CustomAuthenticationScheme, AuthenticationConstants.CustomAuthenticationScheme, options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        string authorization = context.Request.Headers[HeaderNames.Authorization];
                        if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                            return JwtBearerDefaults.AuthenticationScheme;
                        return CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                });

            var clientServiceSettings = configuration.GetSection(nameof(ClientServiceSettings)).Get<ClientServiceSettings>();
            services.AddHttpClient<IGrandPashaBetService, GrandPashaBetService>(configure =>
            {
                configure.BaseAddress = new Uri(clientServiceSettings.GrandPashaBetUrl);
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq(configuration.GetSection("SeqSettings"));
            });

            services.AddTransient<ErrorHandlerMiddleware>();

            return services;
        }
    }
}
