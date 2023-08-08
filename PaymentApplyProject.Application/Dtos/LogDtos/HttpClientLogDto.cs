using Newtonsoft.Json;

namespace PaymentApplyProject.Application.Dtos.LogDtos
{
    public class HttpClientLogDto
    {
        public string Url { get; set; }
        public object Request { get; set; }
        public object Response { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
