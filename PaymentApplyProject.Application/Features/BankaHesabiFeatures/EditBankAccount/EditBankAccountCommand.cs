using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.EditBankAccount
{
    public class EditBankAccountCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public short BankaId { get; set; }
        public string HesapNumarasi { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public decimal UstLimit { get; set; }
        public decimal AltLimit { get; set; }
        public bool AktifMi { get; set; }
    }

    public class EditBankAccountCommandHandler : IRequestHandler<EditBankAccountCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public EditBankAccountCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(EditBankAccountCommand request, CancellationToken cancellationToken)
        {
            var isExistSameAccountNumber = await _paymentContext.BankaHesaplari.AnyAsync(x =>
                x.Id != request.Id
                && x.HesapNumarasi == request.HesapNumarasi
                , cancellationToken);
            if (isExistSameAccountNumber)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameAccountNumber);

            var bankAccount = _customMapper.Map<BankaHesabi>(request);
            _paymentContext.BankaHesaplari.Entry(bankAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}
