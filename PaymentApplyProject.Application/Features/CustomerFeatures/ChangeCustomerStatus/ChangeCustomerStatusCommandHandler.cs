using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Interfaces;
using PaymentApplyProject.Application.Localizations;

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
            var customer = await _paymentContext.Customers.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted, cancellationToken);
            if (customer is null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.MusteriBulunamadi);

            customer.Active = !customer.Active;
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.IslemBasarili);
        }
    }
}
