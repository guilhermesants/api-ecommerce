using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Application.Common.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(e => e.ErrorMessage).ToArray()
                );

            var problemDetails = new ValidationProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.21",
                Title = "Uma ou mais falhas de validação ocorreram.",
                Status = StatusCodes.Status422UnprocessableEntity,
                Errors = errors,
                Instance = Guid.NewGuid().ToString()
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            var result = JsonConvert.SerializeObject(problemDetails);
            return context.Response.WriteAsync(result);
        }
        else
        {
            var result = JsonConvert.SerializeObject(new { isSuccess = false, error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
