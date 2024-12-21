using ExchangeRate.Application.Contracts.Persistence;
using ExchangeRate.Persistence.DatabaseContext;
using ExchangeRate.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRate.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ExchangeDatabaseContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("ExchangeRateDatabaseConnectionString"));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICurrencyCodeRepository, CurrencyCodeRepository>();

            return services;
        }
    }
}
