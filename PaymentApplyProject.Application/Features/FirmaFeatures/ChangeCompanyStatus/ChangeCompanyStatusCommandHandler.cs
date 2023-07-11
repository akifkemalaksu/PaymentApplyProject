using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;

namespace PaymentApplyProject.Application.Features.FirmaFeatures.ChangeCompanyStatus
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
            var company = await _paymentContext.Firmalar.FirstOrDefaultAsync(x => x.Id == request.Id && !x.SilindiMi, cancellationToken);
            if (company is null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            company.AktifMi = !company.AktifMi;
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
