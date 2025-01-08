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
    }
}
