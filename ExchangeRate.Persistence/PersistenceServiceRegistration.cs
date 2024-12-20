using ExchangeRate.Persistence.DatabaseContext;
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
            services.AddDbContext<ExchangeRateDatabaseContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("ExchangeRateDatabaseConnectionString"));
            });


            return services;
        }
    }
}
