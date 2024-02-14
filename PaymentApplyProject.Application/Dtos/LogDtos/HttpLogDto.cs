using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

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
