using Microsoft.Extensions.DependencyInjection;
using MediatR.NotificationPublishers;
using FluentValidation;
using Application.Features.Money.Commands.GetConvertedMoney;
using System.Reflection;
using Application.Contracts.CurrencyConvert;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<ICurrencyConvert, CurrencyConvert>();

            // services.AddValidatorsFromAssemblyContaining<GetExchangeRateCommandValidator>();

            return services;
        }
    }
}
