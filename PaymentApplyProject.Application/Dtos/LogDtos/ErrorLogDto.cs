namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public class ErrorLogDto : HttpLogDto
    {
        public Exception Exception { get; set; }
    }
}
