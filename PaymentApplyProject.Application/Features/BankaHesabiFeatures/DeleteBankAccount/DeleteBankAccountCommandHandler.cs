using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.DeleteBankAccount
{
    public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public DeleteBankAccountCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            var bankaHesap = await _paymentContext.BankaHesaplari.FirstOrDefaultAsync(x => x.Id == request.Id && !x.SilindiMi, cancellationToken);

            if (bankaHesap == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, Messages.NotFound);

            _paymentContext.BankaHesaplari.Remove(bankaHesap);

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
