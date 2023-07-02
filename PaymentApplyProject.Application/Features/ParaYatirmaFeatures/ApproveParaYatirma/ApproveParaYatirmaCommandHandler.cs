using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.ApproveParaYatirma
{
    public class ApproveParaYatirmaCommandHandler : IRequestHandler<ApproveParaYatirmaCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public ApproveParaYatirmaCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(ApproveParaYatirmaCommand request, CancellationToken cancellationToken)
        {
            var paraYatirma = await _paymentContext.ParaYatirmalar.FirstOrDefaultAsync(x =>
                x.Id == request.Id
                && !x.SilindiMi
                , cancellationToken);

            if (paraYatirma == null)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.NotFound, string.Format(Messages.NotFoundWithName, nameof(ParaYatirma)));

            if (paraYatirma.ParaYatirmaDurumId == ParaYatirmaDurumSabitler.REDDEDILDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyRejected);
            else if (paraYatirma.ParaYatirmaDurumId == ParaYatirmaDurumSabitler.ONAYLANDI)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.AlreadyApproved);

            paraYatirma.ParaYatirmaDurumId = ParaYatirmaDurumSabitler.ONAYLANDI;
            paraYatirma.Tutar = request.Tutar;
            paraYatirma.IslemTarihi = DateTime.Now;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
