using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Domain.Constants
{
    public static class RoleConstants
    {
        public const string ADMIN = "admin";
        public const string USER = "user";
        public const string CUSTOMER = "customer";

        public const short ADMIN_ID = 1;
        public const short USER_ID = 2;
        public const short CUSTOMER_ID = 3;
    }
}
