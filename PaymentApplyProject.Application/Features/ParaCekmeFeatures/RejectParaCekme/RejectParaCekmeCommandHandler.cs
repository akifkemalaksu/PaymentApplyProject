using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.RejectParaCekme
{
    public class RejectParaCekmeCommandHandler : IRequestHandler<RejectParaCekmeCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public RejectParaCekmeCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(RejectParaCekmeCommand request, CancellationToken cancellationToken)
        {
            var paraCekme = await _paymentContext.ParaCekmeler.FirstOrDefaultAsync(x =>
               x.Id == request.Id
               && !x.SilindiMi
               , cancellationToken);

            if (paraCekme == null)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, string.Format(Messages.NotFoundWithName, nameof(ParaCekme)));
            }

            if (paraCekme.ParaCekmeDurumId == ParaCekmeDurumSabitler.REDDEDILDI)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyRejected);
            }
            else if (paraCekme.ParaCekmeDurumId == ParaCekmeDurumSabitler.ONAYLANDI)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyApproved);
            }

            paraCekme.ParaCekmeDurumId = ParaCekmeDurumSabitler.REDDEDILDI;
            paraCekme.IslemTarihi = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
