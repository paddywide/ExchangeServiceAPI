using ExchangeRate.Application;
using ExchangeRate.Persistence;
using ExchangeRate.Infrastructure;
using ExchangeRate.Identity;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI(configuration)
                .AddCoreDI()
                .AddIdentityServices(configuration)
                .AddPersistenceServices(configuration);

            //services.AddValidatorsFromAssemblyContaining<CurrencyConvertRequestValidator>();
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            return services;
        }

    }
}
