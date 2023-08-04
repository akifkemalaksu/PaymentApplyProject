using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Domain.Constants
{
    public static class ErrorCodes
    {
        // 0 - 100 user errors
        public const string NotAuthenticated = "0001";
        public const string UserHasNoCompany = "0002";

        // 100 - 200 company errors
        public const string CompanyIsNotFound = "0101";
        public const string CompanyIsNotActive = "0102";

        // 200 - 300 customer errors
        public const string CustomerIsNotActive = "0201";
        public const string CustomerIsNotFound = "0202";

        // 300 - 400 deposit errors
        public const string KeyValueIsNull = "0300";
        public const string DepositRequestIsNotFound = "0301";
        public const string DepositRequestHashIsUsed = "0302";
        public const string ThereIsPendingDeposit = "0303";
        public const string DepositCallbackException = "0304";
        public const string DepositRequestIsTimeout = "0305";

        // 400 - 500 withdraw errors

    }
}
