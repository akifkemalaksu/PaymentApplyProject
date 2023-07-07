using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Dtos
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        private static Response<T> Create(HttpStatusCode statusCode, bool isSuccessful, T data, string message) => new Response<T>
        {
            StatusCode = statusCode,
            Data = data,
            Message = message,
            IsSuccessful = isSuccessful
        };

        public static Response<T> Success(HttpStatusCode statusCode, T data, string message) => Create(statusCode, true, data, message);
        public static Response<T> Success(HttpStatusCode statusCode, T data) => Success(statusCode, data, string.Empty);
        public static Response<T> Success(HttpStatusCode statusCode, string message) => Success(statusCode, default, message);
        public static Response<T> Success(HttpStatusCode statusCode) => Success(statusCode, default, string.Empty);

        public static Response<T> Error(HttpStatusCode statusCode, T data, string message) => Create(statusCode, false, data, message);
        public static Response<T> Error(HttpStatusCode statusCode, T data) => Error(statusCode, data, string.Empty);
        public static Response<T> Error(HttpStatusCode statusCode, string message) => Error(statusCode, default, message);
        public static Response<T> Error(HttpStatusCode statusCode) => Error(statusCode, default, string.Empty);
    }

    public class NoContent { }
}
