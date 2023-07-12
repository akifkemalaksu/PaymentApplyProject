using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Features.CompanyFeatures.ChangeCompanyStatus;

namespace PaymentApplyProject.Application.Features.CompanyFeatures.ChangeCompanyStatus
{
    public class ChangeCompanyStatusCommandHandler : IRequestHandler<ChangeCompanyStatusCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public ChangeCompanyStatusCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(ChangeCompanyStatusCommand request, CancellationToken cancellationToken)
        {
            var company = await _paymentContext.Companies.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Delete, cancellationToken);
            if (company is null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            company.Active = !company.Active;
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
