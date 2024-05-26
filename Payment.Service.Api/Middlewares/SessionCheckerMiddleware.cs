using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace Payment.Service.Api.Middlewares
{
    public class SessionCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IMemoryCache _memCache;

        public SessionCheckerMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memCache= memoryCache;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment environment)
        {
            string token = context.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrEmpty(token))
            {
                var sessionId = GetSessionIdFromToken(token);

                var session = _memCache.Get(sessionId);

                if (session == null)
                    throw new UnauthorizedAccessException("Сессия закрыта");
            }

            await _next(context);
        }

        private string GetSessionIdFromToken(string token)
        {
            // Парсинг токена
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedToken = tokenHandler.ReadJwtToken(token.Replace("Bearer ", ""));

            // Получение утверждений из токена
            var claims = parsedToken.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var sessionId = claims.FirstOrDefault(x => x.Type == "SessionId").Value;

            return sessionId;
        }
    }
}
