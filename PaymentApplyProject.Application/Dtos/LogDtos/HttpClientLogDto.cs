using Newtonsoft.Json;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public class HttpClientLogDto
    {
        public int StatusCode { get; set; }
        public string Url { get; set; }
        public object Request { get; set; }
        public object Response { get; set; }
    }
}
