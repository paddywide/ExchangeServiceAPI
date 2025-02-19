﻿using Core.Interfaces;
using ExchangeRate.Application.EventHandlers.Domain;
using ExchangeRate.Application.Interfaces;
using ExchangeRate.Domain.Event;
using ExchangeRate.Infrastructure.EventDispatching;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ExchangeRate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IExternalVendorRepository, ExternalVendorRepository>();

            services.AddHttpClient<IExchangeServiceHttpClientService, ExchangeServiceHttpClientService>();
            //services.AddHttpClient<ExchangeServiceHttpClientService>((serviceProvider, httpClient) =>
            //{
            //    httpClient.BaseAddress = new Uri(configuration.GetConnectionString("ExchangeRateUri"));
            //});
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IEventHandler<HistoryItemAddedEvent>, HistoryItemAddedEventHandler>();

            return services;
        }
    }
}
