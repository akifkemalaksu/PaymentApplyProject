using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.MusteriFeatures.ChangeCustomerStatus
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
            var customer = await _paymentContext.Musteriler.FirstOrDefaultAsync(x => x.Id == request.Id && !x.SilindiMi, cancellationToken);
            if (customer is null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            customer.AktifMi = !customer.AktifMi;
            _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
