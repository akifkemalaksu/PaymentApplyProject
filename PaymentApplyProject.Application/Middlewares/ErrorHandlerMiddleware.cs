using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Dtos.ResponseDtos;
using PaymentApplyProject.Application.Exceptions;
using PaymentApplyProject.Application.Helpers;
using System.Net;

namespace PaymentApplyProject.Application.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ErrorHandlerMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (CallbackException callbackEx)
            {
                await HandleCallbackExceptionAsync(context, callbackEx);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleCallbackExceptionAsync(HttpContext httpContext, CallbackException callbackEx)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await httpContext.Request.Body.CopyToAsync(requestStream);

            var log = new HttpLogDto
            {
                Exception = callbackEx,
                Request = new RequestLogDto
                {
                    Body = StreamHelper.ReadStreamInChunks(requestStream),
                    Host = httpContext.Request.Host.ToString(),
                    Path = httpContext.Request.Path,
                    QueryString = httpContext.Request.QueryString.ToString(),
                    Method = httpContext.Request.Method,
                },
                Response = new ResponseLogDto
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Content = callbackEx.Message
                }
            };
            _logger.LogError(callbackEx, "{@log}", log);

            await httpContext.Response.WriteAsync(Response<NoContent>.Error(HttpStatusCode.InternalServerError, callbackEx.Message, callbackEx.ErrorCode).ToString());
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await httpContext.Request.Body.CopyToAsync(requestStream);

            var log = new HttpLogDto
            {
                Exception = ex,
                Request = new RequestLogDto
                {
                    Body = StreamHelper.ReadStreamInChunks(requestStream),
                    Host = httpContext.Request.Host.ToString(),
                    Path = httpContext.Request.Path,
                    QueryString = httpContext.Request.QueryString.ToString(),
                    Method = httpContext.Request.Method,
                },
                Response = new ResponseLogDto
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Content = ex.Message
                }
            };

            _logger.LogError(ex, "{@log}", log);

            await httpContext.Response.WriteAsync(Response<NoContent>.Error(HttpStatusCode.InternalServerError, ex.Message).ToString());
        }
    }
}
