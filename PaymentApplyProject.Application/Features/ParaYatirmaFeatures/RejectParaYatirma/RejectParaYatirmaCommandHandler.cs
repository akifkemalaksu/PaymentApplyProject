using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.RejectParaYatirma
{
    public class RejectParaYatirmaCommandHandler : IRequestHandler<RejectParaYatirmaCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public RejectParaYatirmaCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(RejectParaYatirmaCommand request, CancellationToken cancellationToken)
        {
            var paraYatirma = await _paymentContext.ParaYatirmalar.FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.SilindiMi
            , cancellationToken);

            if (paraYatirma == null)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, string.Format(Messages.NotFoundWithName, nameof(ParaYatirma)));
            }

            if (paraYatirma.ParaYatirmaDurumId == ParaYatirmaDurumSabitler.REDDEDILDI)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyRejected);
            }
            else if (paraYatirma.ParaYatirmaDurumId == ParaYatirmaDurumSabitler.ONAYLANDI)
            {
                await _paymentContext.RollbackTransactionAsync(cancellationToken);
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyApproved);
            }

            paraYatirma.ParaYatirmaDurumId = ParaYatirmaDurumSabitler.REDDEDILDI;
            paraYatirma.IslemTarihi = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
