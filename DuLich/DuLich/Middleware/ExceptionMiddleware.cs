using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace DuLich.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ProblemDetailsFactory _problemDetailsFactory;

        public ExceptionMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
        {
            _next = next; _problemDetailsFactory = problemDetailsFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var statusCode = error switch
                {
                    AccessViolationException => (int)HttpStatusCode.Forbidden,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    InvalidDataException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var problemDetails = _problemDetailsFactory
                    .CreateProblemDetails(context, statusCode: statusCode, detail: error.Message, instance: context.Request.Path);

                string strJson = JsonSerializer.Serialize(problemDetails);
                context.Response.Headers.Add("Content-Type", "application/json");
                await context.Response.WriteAsync(strJson);
            }
        }
    }
}