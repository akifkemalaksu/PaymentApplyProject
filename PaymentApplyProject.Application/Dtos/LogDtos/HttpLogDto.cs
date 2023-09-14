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
    public class HttpLogDto : LogDto
    {
        public string Schema { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string QueryString { get; set; }
        public object Body { get; set; }
    }
}
