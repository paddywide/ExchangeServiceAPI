using Core.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddScoped<IExternalVendorRepository, ExternalVendorRepository>();

            services.AddHttpClient<IExchangeServiceHttpClientService, ExchangeServiceHttpClientService>();
            //services.AddHttpClient<ExchangeServiceHttpClientService>((serviceProvider, httpClient) =>
            //{
            //    httpClient.BaseAddress = new Uri("https://v6.exchangerate-api.com/v6/53cf2953cdaa1c3f08d63816/latest/AUD");
            //});
                
            return services;
        }
    }
}
