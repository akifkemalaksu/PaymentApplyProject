using MediatR;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Core.Dtos;
using PaymentApplyProject.Domain.Constants;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.ParaCekmeFeatures.AddParaCekme
{
    public class AddParaCekmeCommandHandler : IRequestHandler<AddParaCekmeCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;

        public AddParaCekmeCommandHandler(IPaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Response<NoContent>> Handle(AddParaCekmeCommand request, CancellationToken cancellationToken)
        {
            var firma = _paymentContext.Firmalar.FirstOrDefault(x => x.Ad == request.FirmaAdi && !x.SilindiMi);
            if (firma == null)
            {
                firma = new() { Ad = request.FirmaAdi };
                _paymentContext.Firmalar.Add(firma);
                await _paymentContext.SaveChangesAsync();
            }

            var musteri = _paymentContext.Musteriler.FirstOrDefault(x => x.FirmaId == firma.Id && x.KullaniciAdi == request.KullaniciAdi && !x.SilindiMi);
            if (musteri == null)
            {
                musteri = new() { FirmaId = firma.Id, KullaniciAdi = request.KullaniciAdi };
                _paymentContext.Musteriler.Add(musteri);
                await _paymentContext.SaveChangesAsync();
            }

            var addParaCekme = new ParaCekme { FirmaId = firma.Id, MusteriId = musteri.Id, Tutar = request.Tutar, HesapNumarasi = request.HesapNumarasi, DurumId = ParaCekmeDurumSabitler.BEKLIYOR };

            _paymentContext.ParaCekmeler.Add(addParaCekme);

            await _paymentContext.SaveChangesAsync();

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
