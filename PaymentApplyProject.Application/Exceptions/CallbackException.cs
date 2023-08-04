using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Exceptions
{
    public class CallbackException : Exception
    {
        public string ErrorCode { get; set; }
        public CallbackException(string? message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
