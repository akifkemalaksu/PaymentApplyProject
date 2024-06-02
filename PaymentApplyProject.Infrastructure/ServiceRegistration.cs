using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using PaymentApplyProject.Application.Behaviors;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Dtos.Settings;
using PaymentApplyProject.Application.Features.DepositFeatures.AddDeposit;
using PaymentApplyProject.Application.Features.DepositFeatures.ApproveDeposit;
using PaymentApplyProject.Application.Features.DepositFeatures.DepositRequestsTimeoutControl;
using PaymentApplyProject.Application.Features.DepositFeatures.RejectDeposit;
using PaymentApplyProject.Application.Features.WithdrawFeatures.ApproveWithdraw;
using PaymentApplyProject.Application.Features.WithdrawFeatures.RejectWithdraw;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Services.BackgroundServices;
using PaymentApplyProject.Application.Services.HubServices;
using PaymentApplyProject.Application.Services.InfrastructureServices;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Infrastructure.Mapping.Mapster;
using PaymentApplyProject.Infrastructure.Services.HubServices;
using PaymentApplyProject.Infrastructure.Services.InfrastructureServices;
using System.Text;

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
            services.AddSingleton<IDepositPaymentHubRedirectionService, DepositPaymentHubRedirectionService>();
            services.AddSingleton<ICacheService, InMemoryCacheService>();
            services.AddSingleton<IHubConnectionUniqueKeyCacheService, HubConnectionUniqueKeyCacheService>();
            services.AddSingleton<IMailSenderService, MailSenderService>();
            services.AddSingleton<DepositRequestControlBackgroundService>();

            services.AddHttpClient<IRequestHandler<AddDepositCommand, Response<AddDepositResult>>, AddDepositCommandHandler>();
            services.AddHttpClient<IRequestHandler<ApproveDepositCommand, Response<NoContent>>, ApproveDepositCommandHandler>();
            services.AddHttpClient<IRequestHandler<RejectDepositCommand, Response<NoContent>>, RejectDepositCommandHandler>();
            services.AddHttpClient<IRequestHandler<ApproveWithdrawCommand, Response<NoContent>>, ApproveWithdrawCommandHandler>();
            services.AddHttpClient<IRequestHandler<RejectWithdrawCommand, Response<NoContent>>, RejectWithdrawCommandHandler>();
            services.AddHttpClient<IRequestHandler<DepositRequestsTimeoutControlCommand, Response<NoContent>>, DepositRequestsTimeoutControlCommandHandler>();

            services.AddHostedService(provider => provider.GetRequiredService<DepositRequestControlBackgroundService>());

            services.Configure<ClientIntegrationSettings>(configuration.GetSection(nameof(ClientIntegrationSettings)));
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<ClientIntegrationSettings>>().Value);

            services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<SmtpSettings>>().Value);

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
                        return !string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer ")
                            ? JwtBearerDefaults.AuthenticationScheme
                            : CookieAuthenticationDefaults.AuthenticationScheme;
                    };
                });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq(configuration.GetSection("SeqSettings"));
            });

            return services;
        }
    }
}
