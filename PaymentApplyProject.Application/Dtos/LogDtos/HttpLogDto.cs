using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public class HttpLogDto
    {
        public string MachineName { get; } = Environment.MachineName;
        public string OSVersion { get; } = Environment.OSVersion.ToString();
        public string Schema { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string Body { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public class ErrorLogDto: HttpLogDto
    {
        public Exception Exception { get; set; }
    }
}
