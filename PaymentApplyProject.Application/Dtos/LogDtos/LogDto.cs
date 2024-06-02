namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public abstract class LogDto
    {
        public string MachineName { get; } = Environment.MachineName;
    }
}
