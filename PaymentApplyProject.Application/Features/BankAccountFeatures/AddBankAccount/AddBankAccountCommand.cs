using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PaymentApplyProject.Application.Dtos.ResponseDtos;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.AddBankAccount
{
    public class AddBankAccountCommand : IRequest<Response<NoContent>>
    {
        public short BankId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
    }
}
