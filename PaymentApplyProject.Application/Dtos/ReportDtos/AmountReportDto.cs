namespace PaymentApplyProject.Application.Dtos.ReportDtos
{
    public class AmountReportDto
    {
        public decimal DepositAmounts { get; set; }
        public decimal ApprovedDepositAmounts { get; set; }
        public decimal PendingDepositAmounts { get; set; }
        public decimal WithdrawAmounts { get; set; }
        public decimal ApprovedWithdrawAmounts { get; set; }
        public decimal PendingWithdrawAmounts { get; set; }

        public int ApprovedDepositPercent => (int)(100 * ApprovedDepositAmounts / (DepositAmounts == 0 ? 1 : DepositAmounts));
        public int PendingDepositPercent => (int)(100 * PendingDepositAmounts / (DepositAmounts == 0 ? 1 : DepositAmounts));

        public int ApprovedWithdrawPercent => (int)(100 * ApprovedWithdrawAmounts / (WithdrawAmounts == 0 ? 1 : WithdrawAmounts));
        public int PendingWithdrawPercent => (int)(100 * PendingWithdrawAmounts / (WithdrawAmounts == 0 ? 1 : WithdrawAmounts));
    }
}
