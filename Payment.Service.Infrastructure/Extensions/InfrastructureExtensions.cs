using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Service.Domain.Abstractions;
using Payment.Service.Infrastructure.Persistense.Context;
using Payment.Service.Infrastructure.Repositories;

namespace Payment.Service.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {
             return service.AddDbContext<PaymentDbContext>(options =>
             {
                 options.ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.AmbientTransactionWarning));
                        options.UseSqlite(configuration.GetConnectionString("PaymentDb"),
                                          x => x.MigrationsAssembly("Payment.Service.Api")); 
             }).AddScoped<IUserRepository, UserRepository>()
               .AddScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}