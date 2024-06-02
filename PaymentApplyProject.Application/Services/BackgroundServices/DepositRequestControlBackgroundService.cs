using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Features.DepositFeatures.DepositRequestsTimeoutControl;

namespace PaymentApplyProject.Infrastructure.Services.BackgroundServices
{
    public record DepositRequestControlBackgroundServiceState(bool IsEnabled);

    public class DepositRequestControlBackgroundService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromMinutes(15);
        private readonly ILogger<DepositRequestControlBackgroundService> _logger;
        private readonly IServiceScopeFactory _factory;
        private int _executionCount = 0;
        public bool IsEnabled { get; set; } = true;

        public DepositRequestControlBackgroundService(ILogger<DepositRequestControlBackgroundService> logger, IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var name = nameof(DepositRequestControlBackgroundService);

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

                        var log = new BackgroundServiceLogDto
                        {
                            ExecutionCount = _executionCount,
                            Name = name,
                        };
                        _logger.LogInformation("{@log}", log);
                    }
                }
                catch (Exception ex)
                {
                    var log = new BackgroundServiceLogDto
                    {
                        ExecutionCount = _executionCount,
                        Name = name,
                    };
                    _logger.LogError(ex, "{@log}", log);
                }
            }
        }
    }
}
