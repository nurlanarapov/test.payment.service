using Microsoft.IdentityModel.Tokens;
using Payment.Service.Api.Models;
using System.Net;
using System.Security.Authentication;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Payment.Service.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context, IWebHostEnvironment environment)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, environment);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment environment)
        {
            var response = context.Response;
            Result<object> body = new Result<object>();

            switch (ex)
            {
                case UnauthorizedAccessException e:
                    body.MessageKey = "Forbidden";

                    if (environment.IsDevelopment())
                        body.Message = e.Message;
                    else
                        body.Message = "Access Denied";

                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case ArgumentException e:
                    body.MessageKey = "ArgumentException";
                    body.Message = e.Message;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case AuthenticationException:
                case SecurityTokenException:
                    body.MessageKey = "Unauthorized";
                    body.Message = ex.Message;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    body.MessageKey = "InternalServerError";
                    if (environment.IsDevelopment())
                        body.Message = ex.Message;
                    else
                        body.Message = "Internal Server Error";

                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError($"{ex.InnerException?.Message ?? ex.Message}");
                    break;
            }
            body.Success = false;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            response.ContentType= "application/json";
            var result = JsonSerializer.Serialize(body, options);
            await response.WriteAsync(result);
        }
    }
}
