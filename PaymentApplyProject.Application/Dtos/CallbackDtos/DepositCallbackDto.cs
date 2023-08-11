using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.CallbackDtos
{
    public class DepositCallbackBodyDto
    {
        public string MethodType { get; set; }
        public int ExternalTransactionId { get; set; }
        public string UniqueTransactionId { get; set; }
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
    }
}
