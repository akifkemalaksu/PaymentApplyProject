using PaymentApplyProject.Application.Dtos.ReportDtos;

namespace PaymentApplyProject.Application.Features.ReportsFeatures.GetMainReports
{
    public class GetMainReportsResult
    {
        public required AmountReportDto TotalReport { get; set; }
        public required AmountReportDto DailyReport { get; set; }
    }
}
