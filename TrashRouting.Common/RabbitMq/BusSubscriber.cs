using System;
using Microsoft.AspNetCore.Builder;
using RawRabbit;
using TrashRouting.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Context;

namespace TrashRouting.Common.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IBusClient busClient;

        public BusSubscriber(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            busClient = serviceProvider.GetService<IBusClient>();
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string rabbitNamespace = null, string queueName = null, Func<TCommand> onError = null) where TCommand : ICommand
        {
            busClient.SubscribeAsync<TCommand>(async (command, context) =>
            {
                var handler = serviceProvider.GetService<ICommandHandler<TCommand>>();
                await handler.HandleAsync(command);
            });

            return this;
        }
    }
}
