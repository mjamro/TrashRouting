using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using System;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;

namespace TrashRouting.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IBusClient busClient;

        public BusSubscriber(IApplicationBuilder app)
        {
            serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            busClient = serviceProvider.GetService<IBusClient>();
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string rabbitNamespace = null, string queueName = null, Func<TCommand> onError = null) where TCommand : ICommand
        {
            busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, context) =>
            {
                var handler = serviceProvider.GetService<ICommandHandler<TCommand>>();
                await handler.HandleAsync(command);
            });

            return this;
        }

        public IBusSubscriber SubscribeEvent<TEvent>(string rabbitNamespace = null, string queueName = null, Func<TEvent> onError = null) where TEvent : IEvent
        {
            busClient.SubscribeAsync<TEvent, CorrelationContext>(async (command, context) =>
            {
                var handler = serviceProvider.GetService<IEventHandler<TEvent>>();
                await handler.HandleAsync(command);
            });

            return this;
        }
    }
}
