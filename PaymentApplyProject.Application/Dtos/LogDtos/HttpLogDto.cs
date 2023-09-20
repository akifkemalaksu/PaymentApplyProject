using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public abstract class HttpLogDto : LogDto
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
    }

    public class RequestLogDto : HttpLogDto
    {
        public string Method { get; set; }
        public object Body { get; set; }
    }

    public class ResponseLogDto : HttpLogDto
    {
        public int StatusCode { get; set; }
        public object Content { get; set; }
    }
}
