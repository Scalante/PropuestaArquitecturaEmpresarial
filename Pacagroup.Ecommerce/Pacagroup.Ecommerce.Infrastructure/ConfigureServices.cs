using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Options;
using Pacagroup.Ecommerce.Infrastructure.EventBus.Options;
using Pacagroup.Ecommerce.Application.Interface.Infrastructure;
using Pacagroup.Ecommerce.Infrastructure.EventBus;
using SendGrid.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Infrastructure.Notification.Options;
using Pacagroup.Ecommerce.Infrastructure.Notification;

namespace Pacagroup.Ecommerce.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.ConfigureOptions<RabbitMqOptionsSetup>();
            services.AddScoped<IEventBus, EventBusRabbitMQ>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqOptions? opt = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RabbitMqOptions>>()
                        .Value;

                    cfg.Host(opt.HostName, opt.VirtualHost, h =>
                    {
                        h.Username(opt.UserName);
                        h.Password(opt.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            /*Servicio de SendGrid*/
            services.AddScoped<INotification, NotificationSendGrid>();
            services.ConfigureOptions<SendgridOptionsSetup>();
            SendgridOptions? sendgridOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<SendgridOptions>>()
                .Value;

            services.AddSendGrid((options =>
            {
                options.ApiKey = sendgridOptions.ApiKey;
            }));

            return services;
        }
    }
}