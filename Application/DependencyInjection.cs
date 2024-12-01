using Microsoft.Extensions.DependencyInjection;
using MediatR.NotificationPublishers;
using Application.Validators;
using FluentValidation;

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
            services.AddValidatorsFromAssemblyContaining<CurrencyConvertRequestValidator>();

            return services;
        }
    }
}
