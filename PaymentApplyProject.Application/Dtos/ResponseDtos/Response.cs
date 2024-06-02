using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public required T Data { get; set; }
        [DataMember]
        public required string Message { get; set; }
        [DataMember]
        public required string ErrorCode { get; set; }


        private static Response<T> Create(HttpStatusCode statusCode, bool isSuccessful, T data, string message, string? errorCode)
        {
            return new()
            {
                StatusCode = statusCode,
                Data = data,
                Message = message,
                IsSuccessful = isSuccessful,
                ErrorCode = errorCode
            };
        }

        public static Response<T> Success(HttpStatusCode statusCode, T data, string message)
        {
            return Create(statusCode, true, data, message, null);
        }

        public static Response<T> Success(HttpStatusCode statusCode, T data)
        {
            return Success(statusCode, data, string.Empty);
        }

        public static Response<T> Success(HttpStatusCode statusCode, string message)
        {
            return Success(statusCode, default, message);
        }

        public static Response<T> Success(HttpStatusCode statusCode)
        {
            return Success(statusCode, string.Empty);
        }

        public static Response<T> Error(HttpStatusCode statusCode, string message, string? errorCode)
        {
            return Create(statusCode, false, default, message, errorCode);
        }

        public static Response<T> Error(HttpStatusCode statusCode, string message)
        {
            return Error(statusCode, message, null);
        }

        public static Response<T> Error(HttpStatusCode statusCode)
        {
            return Error(statusCode, message: string.Empty);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }

    public class NoContent { }
}
