using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PixelPlus.Application.Exceptions;
using PixelPlus.Application.Models;
using PixelPlus.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PixelPlus.Api.Middelware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            if (ex is TypeInitializationException)
            {
                ex = ex.InnerException;
            }

            HttpStatusCode statusCode;
            var exType = ex.GetType();
            object content = null;
            switch (exType)
            {
                case { } when exType == typeof(NotFoundException):
                    statusCode = HttpStatusCode.NotFound;
                    var notFoundException = ex as NotFoundException;
                    if (!string.IsNullOrEmpty(notFoundException.Key))
                    {
                        content = new ErrorResponse(notFoundException);
                    }

                    break;

                case { } when exType == typeof(BusinessException) || exType.BaseType == typeof(BusinessException):
                    statusCode = HttpStatusCode.BadRequest;
                    var errorResponse = new ErrorResponse(ex as BusinessException);
                    content = errorResponse;
                    _logger.LogWarning(ex, $"[Bad Request] {JsonConvert.SerializeObject(errorResponse)}, ex: {ex.Message}, inner ex: {ex.InnerException?.Message}");
                    break;


                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    _logger.LogCritical(new EventId(0, "UNKNOWN_EXCEPTION"), ex, $"unknown exception: {ex.Message}");
                    break;
            }

            httpContext.Response.StatusCode = statusCode.GetHashCode();

            if (content != null)
            {
                await httpContext.Response.WriteAsJsonAsync(content);
            }

            await httpContext.Response.CompleteAsync();
        }
    }
}
