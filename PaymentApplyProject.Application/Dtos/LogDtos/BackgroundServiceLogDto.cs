namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    internal class BackgroundServiceLogDto : LogDto
    {
        public required string Name { get; set; }
        public int ExecutionCount { get; set; }
    }
}
