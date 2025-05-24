using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVPv5.API.Middleware
{
    public class ExMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExMiddleware> _logger;

        public ExMiddleware(RequestDelegate next, ILogger<ExMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка");

                context.Response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    //Дописывать
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    ValidationException => StatusCodes.Status400BadRequest,
                    DbUpdateException => StatusCodes.Status409Conflict,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    NotImplementedException => StatusCodes.Status501NotImplemented,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = statusCode;

                var problem = new ProblemDetails
                {
                    Status = statusCode,
                    Title = GetTitleForStatus(statusCode),
                    Detail = ex.Message,
                    Instance = context.Request.Path
                };

                await context.Response.WriteAsJsonAsync(problem);
            }
        }

        private static string GetTitleForStatus(int status) => status switch
        {
            StatusCodes.Status400BadRequest => "Ошибка валидации",
            StatusCodes.Status404NotFound => "Контент не найден",
            StatusCodes.Status409Conflict => "Проблема при работе с данными",
            StatusCodes.Status500InternalServerError => "Внутренняя ошибка сервера",
            StatusCodes.Status501NotImplemented => "Метод не реализован",
            _ => "Произошла ошибка"
        };
    }
}
