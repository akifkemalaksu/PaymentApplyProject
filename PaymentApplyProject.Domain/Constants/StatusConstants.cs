namespace PaymentApplyProject.Domain.Constants
{
    public static class StatusConstants
    {
        public const int DEPOSIT_BEKLIYOR = 1;
        public const int DEPOSIT_REDDEDILDI = 2;
        public const int DEPOSIT_ONAYLANDI = 3;


        public const int WITHDRAW_BEKLIYOR = 4;
        public const int WITHDRAW_REDDEDILDI = 5;
        public const int WITHDRAW_ONAYLANDI = 6;

        public const string PENDING = "pending";
        public const string REJECTED = "rejected";
        public const string APPROVED = "approved";
    }
}
