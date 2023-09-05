using MediatR;
using Microsoft.Extensions.Logging;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Pipelines;

namespace PaymentApplyProject.Application.Pipelines
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactional
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IPaymentContext _paymentContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IPaymentContext paymentContext)
        {
            _logger = logger;
            _paymentContext = paymentContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                await _paymentContext.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation($"Begin transaction: {typeof(TRequest).Name}.");
                    await _paymentContext.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _paymentContext.CommitTransactionAsync(cancellationToken);
                    _logger.LogInformation($"End transaction: {typeof(TRequest).Name}.");
                });
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}.");
                _logger.LogError(e.Message, e.StackTrace);
                await _paymentContext.RollbackTransactionAsync(cancellationToken);

                throw;
            }

            return response;
        }
    }
}
