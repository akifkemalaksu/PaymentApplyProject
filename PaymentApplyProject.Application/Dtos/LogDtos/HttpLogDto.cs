using Newtonsoft.Json;
using System.Text.Json;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public class HttpLogDto : LogDto
    {
        public required RequestLogDto Request { get; set; }
        public required ResponseLogDto Response { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Exception? Exception { get; set; }
    }

    public class RequestLogDto
    {
        public required string Host { get; set; }
        public required string Path { get; set; }
        public required string QueryString { get; set; }
        public required string Method { get; set; }
        public required object Body { get; set; }
    }

    public class ResponseLogDto
    {
        public int StatusCode { get; set; }
        public required object Content { get; set; }
    }
}
