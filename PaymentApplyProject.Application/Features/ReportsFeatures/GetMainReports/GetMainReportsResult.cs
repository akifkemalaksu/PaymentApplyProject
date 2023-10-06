using PaymentApplyProject.Application.Dtos.ReportDtos;

namespace PaymentApplyProject.Application.Features.ReportsFeatures.GetMainReports
{
    public class GetMainReportsResult
    {
        public AmountReportDto TotalReport { get; set; }
        public AmountReportDto DailyReport { get; set; }
    }
}
