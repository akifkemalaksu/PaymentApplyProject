﻿using PaymentApplyProject.Domain.Entities;
using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Context;
using Microsoft.EntityFrameworkCore;
using PaymentApplyProject.Application.Localizations;
using PaymentApplyProject.Application.Mapping;
using PaymentApplyProject.Application.Features.BankAccountFeatures.AddBankAccount;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.AddBankAccount
{
    public class AddBankAccountCommandHandler : IRequestHandler<AddBankAccountCommand, Response<NoContent>>
    {
        private readonly IPaymentContext _paymentContext;
        private readonly ICustomMapper _customMapper;

        public AddBankAccountCommandHandler(IPaymentContext paymentContext, ICustomMapper customMapper)
        {
            _paymentContext = paymentContext;
            _customMapper = customMapper;
        }

        public async Task<Response<NoContent>> Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
        {
            var isExistSameBankAccount = await _paymentContext.BankAccounts.AnyAsync(x =>
                x.AccountNumber == request.AccountNumber
                && !x.Delete, cancellationToken);
            if (isExistSameBankAccount)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsSameAccountNumber);

            var isExistsRange = await _paymentContext.BankAccounts.AnyAsync(x =>
                x.BankId == request.BankId
                && x.LowerLimit <= request.LowerLimit && x.UpperLimit >= request.LowerLimit
                && x.LowerLimit <= request.UpperLimit && x.UpperLimit >= request.UpperLimit
            , cancellationToken);
            if (isExistsRange)
                return Response<NoContent>.Error(System.Net.HttpStatusCode.BadRequest, Messages.ThereIsPriceRange);

            var bankAccount = _customMapper.Map<BankAccount>(request);
            bankAccount.Active = true;

            await _paymentContext.BankAccounts.AddAsync(bankAccount, cancellationToken);
            await _paymentContext.SaveChangesAsync(cancellationToken);

            return Response<NoContent>.Success(System.Net.HttpStatusCode.OK, Messages.OperationSuccessful);
        }
    }
}