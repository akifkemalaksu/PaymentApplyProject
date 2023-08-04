using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Exceptions
{
    public class CallbackException : Exception
    {
        public CallbackException(string? message) : base(message)
        {
        }
    }
}
