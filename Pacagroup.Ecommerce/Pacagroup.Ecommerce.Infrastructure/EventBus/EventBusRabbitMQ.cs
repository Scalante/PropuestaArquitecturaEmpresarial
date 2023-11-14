using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pacagroup.Ecommerce.Application.Interface.Infrastructure;
using MassTransit;

namespace Pacagroup.Ecommerce.Infrastructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async void Publish<T>(T @event)
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}
