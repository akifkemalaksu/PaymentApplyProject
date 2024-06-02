namespace PaymentApplyProject.Domain.Constants
{
    public static class RoleConstants
    {
        public static string ADMIN => "admin";
        public static string USER => "user";
        public static string CUSTOMER => "customer";
        public static string ACCOUNTING => "accounting";

        public static short ADMIN_ID => 1;
        public static short USER_ID => 2;
        public static short CUSTOMER_ID => 3;
        public static short ACCOUNTING_ID => 4;

        public static short[] PANEL_ROLE_IDS => new[]
        {
            ADMIN_ID,
            USER_ID,
            ACCOUNTING_ID
        };
    }
}
