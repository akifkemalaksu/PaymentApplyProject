using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.BankAccountFeatures.EditBankAccount
{
    public class EditBankAccountCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
        public short BankId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal LowerLimit { get; set; }
        public bool Active { get; set; }
    }
}
