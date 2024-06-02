namespace PaymentApplyProject.Domain.Constants
{
    public static class ErrorCodes
    {
        // 0 - 100 user errors
        public const string UserNotFound = "0001";
        public const string UserHasNoAuthorization = "0002";
        public const string NotAuthenticated = "0003";
        public const string UserHasNoCompany = "0004";

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
        public const string ThereIsDepositSameTransactionId = "0306";
        public const string DepositIsNotFound = "0307";

        // 400 - 500 withdraw errors
        public const string ThereIsPendingWithdraw = "0401";
        public const string WithdrawCallbackException = "0402";
        public const string ThereIsWithdrawSameTransactionId = "0403";
        public const string WithdrawIsNotFound = "0404";
    }
}
