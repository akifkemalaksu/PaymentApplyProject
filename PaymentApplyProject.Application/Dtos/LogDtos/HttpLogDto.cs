using Newtonsoft.Json;
using System.Text.Json;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public class HttpLogDto : LogDto
    {
        public RequestLogDto Request { get; set; }
        public ResponseLogDto Response { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Exception? Exception { get; set; }
    }

    public class RequestLogDto
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Method { get; set; }
        public object Body { get; set; }
    }

    public class ResponseLogDto
    {
        public int StatusCode { get; set; }
        public object Content { get; set; }
    }
}
