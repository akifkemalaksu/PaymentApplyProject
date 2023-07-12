using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Features.CustomerFeatures.ChangeCustomerStatus;

namespace PaymentApplyProject.Application.Features.CustomerFeatures.ChangeCustomerStatus
{
    public class ChangeCustomerStatusCommandHandler : IRequestHandler<ChangeCustomerStatusCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public ChangeCustomerStatusCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(ChangeCustomerStatusCommand request, CancellationToken cancellationToken)
        {
            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Delete, cancellationToken);
            if (customer is null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            customer.Active = !customer.Active;
            _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
