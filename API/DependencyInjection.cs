﻿using ExchangeRate.Application;
using ExchangeRate.Persistence;
using ExchangeRate.Infrastructure;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI(configuration)
                .AddCoreDI()
                .AddPersistenceServices(configuration);

            //services.AddValidatorsFromAssemblyContaining<CurrencyConvertRequestValidator>();
            return services;
        }
    }
}
