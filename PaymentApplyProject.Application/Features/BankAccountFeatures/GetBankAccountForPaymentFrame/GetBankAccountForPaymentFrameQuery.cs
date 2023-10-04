using MediatR;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame
{
    public class GetBankAccountForPaymentFrameQuery : IRequest<Response<GetBankAccountForPaymentFrameResult>>
    {
        public short BankId { get; set; }
        public int DepositRequestId { get; set; }
    }
}
