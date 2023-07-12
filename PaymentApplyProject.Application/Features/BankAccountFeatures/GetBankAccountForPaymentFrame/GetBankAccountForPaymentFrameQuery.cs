using MediatR;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.GetBankAccountForPaymentFrame
{
    public class GetBankAccountForPaymentFrameQuery : IRequest<Response<GetBankAccountForPaymentFrameResult>>
    {
        public int BankId { get; set; }
        public decimal Amount { get; set; }
    }
}
