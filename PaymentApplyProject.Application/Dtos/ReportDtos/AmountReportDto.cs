using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int ApprovedDepositPercent
        {
            get => (int)(100 * ApprovedDepositAmounts / (DepositAmounts == 0 ? 1 : DepositAmounts));
        }
        public int PendingDepositPercent
        {
            get => (int)(100 * PendingDepositAmounts / (DepositAmounts == 0 ? 1 : DepositAmounts));
        }

        public int ApprovedWithdrawPercent
        {
            get => (int)(100 * ApprovedWithdrawAmounts / (WithdrawAmounts == 0 ? 1 : WithdrawAmounts));
        }
        public int PendingWithdrawPercent
        {
            get => (int)(100 * PendingWithdrawAmounts / (WithdrawAmounts == 0 ? 1 : WithdrawAmounts));
        }
    }
}
