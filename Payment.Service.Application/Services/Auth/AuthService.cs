using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Payment.Service.Application.Dto.Auth;
using Payment.Service.Domain.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Payment.Service.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        public AuthService(IUserRepository userRepository, 
                           IConfiguration configuration,
                           IMemoryCache memoryCache) 
        {
            _userRepository= userRepository;
            _configuration= configuration;
            _memoryCache= memoryCache;
        }

        public string Login(LoginDto loginDto)
        {
            var isSuccess = _userRepository.CheckPassword(loginDto.UserName, loginDto.Password);

            if (isSuccess)
                return CreateSession(loginDto.UserName, _configuration["jwt:secretKey"]);                 
            else throw new SecurityTokenException("Неверный логин или пароль");
        }

        public void Logout(string token)
        {
            CloseSession(token);
        }

        private string CreateSession(string username, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            string sessionId = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("SessionId", sessionId)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _configuration["jwt:issuer"],
                Audience = _configuration["jwt:issuer"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string access_token = tokenHandler.WriteToken(token);

            _memoryCache.Set(sessionId, access_token);

            return access_token;
        }

        private void CloseSession(string token)
        {
            // Парсинг токена
            var tokenHandler = new JwtSecurityTokenHandler();
            var parsedToken = tokenHandler.ReadJwtToken(token.Replace("Bearer ", ""));

            // Получение утверждений из токена
            var claims = parsedToken.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var sessionId = claims.FirstOrDefault(x => x.Type == "SessionId").Value;

            _memoryCache.Remove(sessionId);
        }
    }
}
