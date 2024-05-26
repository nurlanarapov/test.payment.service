using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Text;

namespace Payment.Service.Api.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApiExtensions(this IServiceCollection collection, IConfiguration configuration) 
        {
            string key = configuration["jwt:secretKey"];
            var keyBytes = Encoding.UTF8.GetBytes(key);

            collection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["jwt:issuer"],
                    ValidateAudience = false
                };
            });

            return collection;
        }
    }
}
