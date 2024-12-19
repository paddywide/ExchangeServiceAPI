using Application;
using Infrastructure;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI(configuration)
                .AddCoreDI();

            //services.AddValidatorsFromAssemblyContaining<CurrencyConvertRequestValidator>();
            return services;
        }
    }
}
