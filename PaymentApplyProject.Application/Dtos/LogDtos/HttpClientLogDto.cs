namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public class HttpClientLogDto : LogDto
    {
        public int StatusCode { get; set; }
        public required string Url { get; set; }
        public required object Request { get; set; }
        public required object Response { get; set; }
    }
}
