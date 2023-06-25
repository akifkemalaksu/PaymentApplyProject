using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme;

namespace PaymentApplyProject.Application.Features.ParaYatirmaFeatures.AddParaYatirma
{
    public class AddParaYatirmaCommandHandler : IRequestHandler<AddParaYatirmaCommand, Response<AddParaYatirmaResult>>
    {
        private readonly IPaymentContext _paymentContext;

        public AddParaYatirmaCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<AddParaYatirmaResult>> Handle(AddParaYatirmaCommand request, CancellationToken cancellationToken)
        {
            var paraYatirma = new ParaYatirma
            {
                MusteriId = request.MusteriId,
                ParaYatirmaDurumId = ParaYatirmaDurumSabitler.BEKLIYOR,
                BankaHesabiId = request.BankaHesapId,
                Tutar = request.Tutar,
            };
            await _paymentContext.ParaYatirmalar.AddAsync(paraYatirma);
            await _paymentContext.SaveChangesAsync();

            return Response<AddParaYatirmaResult>.Success(System.Net.HttpStatusCode.OK,
                new()
                {
                    ParaYatirmaTalepId = paraYatirma.Id
                },
                Messages.OperationSuccessful
                );
        }
    }
}
