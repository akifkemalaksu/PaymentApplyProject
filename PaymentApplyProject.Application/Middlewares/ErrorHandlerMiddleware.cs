using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.LogDtos;
using PaymentApplyProject.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await httpContext.Request.Body.CopyToAsync(requestStream);

            _logger.LogError(new ErrorLogDto
            {
                Body = StreamHelper.ReadStreamInChunks(requestStream),
                Host = httpContext.Request.Host.ToString(),
                Path = httpContext.Request.Path,
                QueryString = httpContext.Request.QueryString.ToString(),
                Schema = httpContext.Request.Scheme,
                Exception = ex
            }.ToString());

            httpContext.Response.WriteAsync(Response<NoContent>.Error(HttpStatusCode.InternalServerError, ex.Message).ToString());
        }
    }
}
