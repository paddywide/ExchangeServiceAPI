using ExchangeRate.Application.Contracts.Persistence;
using ExchangeRate.Persistence.DatabaseContext;
using ExchangeRate.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ExchangeRate.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ExchangeDatabaseContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("ExchangeRateDatabaseConnectionString"));
                //below line to supress the warning when we Update-Database in nuget
                options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICurrencyCodeRepository, CurrencyCodeRepository>();
            services.AddScoped<IQueryHistoryRepository, QueryHistoryRepository>();

            return services;
        }
    }
}
