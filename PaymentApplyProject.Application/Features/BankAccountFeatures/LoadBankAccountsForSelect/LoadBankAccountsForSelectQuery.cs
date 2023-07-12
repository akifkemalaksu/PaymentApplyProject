using MediatR;
using PaymentApplyProject.Application.Dtos.SelectDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.LoadBankAccountsForSelect
{
    public class LoadBankAccountsForSelectQuery : SelectParameters, IRequest<SelectResult>
    {
        public int BankId { get; set; }
    }
}
