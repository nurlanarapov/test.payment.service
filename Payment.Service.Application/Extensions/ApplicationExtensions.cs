using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Service.Application.Mapper;
using Payment.Service.Application.Services.Auth;
using Payment.Service.Application.Services.Payment;
using Payment.Service.Application.Services.User;
using Payment.Service.Infrastructure.Extensions;
using Payment.Service.Infrastructure.Persistense.Context;

namespace Payment.Service.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
        {
            return service.AddInfrastructureServices(configuration)
                          .AddAutoMapper(typeof(ApplicationMapperProfile))
                          .AddMemoryCache() // Можно редис использовать, если несколько сервисов
                          .AddScoped<IAuthService, AuthService>()
                          .AddScoped<IUserManager, UserManager>()
                          .AddScoped<IPaymentService, PaymentService>();
        }
    }
}