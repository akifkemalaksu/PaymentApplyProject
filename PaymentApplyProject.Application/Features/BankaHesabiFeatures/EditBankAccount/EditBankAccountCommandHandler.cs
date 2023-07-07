using MediatR;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Context;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Domain.Entities;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.EditBankAccount
{
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
