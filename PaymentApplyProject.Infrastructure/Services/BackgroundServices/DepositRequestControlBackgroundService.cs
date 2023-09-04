using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Features.DepositFeatures.DepositRequestsTimeoutControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PaymentApplyProject.Infrastructure.Services.BackgroundServices
{
    public record DepositRequestControlBackgroundServiceState(bool IsEnabled);

    public class DepositRequestControlBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromMinutes(10);
        private readonly ILogger<DepositRequestControlBackgroundService> _logger;
        private readonly IServiceScopeFactory _factory;
        private int _executionCount = 0;
        public bool IsEnabled { get; set; }

        public DepositRequestControlBackgroundService(ILogger<DepositRequestControlBackgroundService> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (IsEnabled)
                    {
                        await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                        IMediator mediator = asyncScope.ServiceProvider.GetService<IMediator>();
                        await mediator.Send(new DepositRequestsTimeoutControlCommand());

                        _executionCount++;
                        _logger.LogInformation($"Executed {nameof(DepositRequestControlBackgroundService)} - Count: {_executionCount}");
                    }
                    else
                        _logger.LogInformation($"Disabled {nameof(DepositRequestControlBackgroundService)}");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Failed to execute {nameof(DepositRequestControlBackgroundService)} with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
