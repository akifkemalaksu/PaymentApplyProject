using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Dtos.ResponseDtos
{
    [DataContract]
    public class Response<T>
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        [DataMember]
        public bool IsSuccessful { get; set; }
        [DataMember]
        public T Data { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ErrorCode { get; set; }


        private static Response<T> Create(HttpStatusCode statusCode, bool isSuccessful, T data, string message, string errorCode) => new()
        {
            StatusCode = statusCode,
            Data = data,
            Message = message,
            IsSuccessful = isSuccessful,
            ErrorCode = errorCode
        };

        public static Response<T> Success(HttpStatusCode statusCode, T data, string message) => Create(statusCode, true, data, message, null);
        public static Response<T> Success(HttpStatusCode statusCode, T data) => Success(statusCode, data, string.Empty);
        public static Response<T> Success(HttpStatusCode statusCode, string message) => Success(statusCode, default, message);
        public static Response<T> Success(HttpStatusCode statusCode) => Success(statusCode, string.Empty);

        public static Response<T> Error(HttpStatusCode statusCode, string message, string errorCode) => Create(statusCode, false, default, message, errorCode);
        public static Response<T> Error(HttpStatusCode statusCode, string message) => Error(statusCode, message, null);
        public static Response<T> Error(HttpStatusCode statusCode) => Error(statusCode, message: string.Empty);

        public override string ToString() => JsonSerializer.Serialize(this,options: new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }

    public class NoContent { }
}
