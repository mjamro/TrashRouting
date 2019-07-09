using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Kafka;

namespace TrashRouting.Sync.Services.Background
{
    public class ProcessEventBackgroundService<TEvent> : BackgroundService where TEvent : IEvent
    {
        private readonly ConsumerConfig consumerConfig;
        private readonly IServiceProvider serviceProvider;

        public ProcessEventBackgroundService(
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            this.consumerConfig = new ConsumerConfig();
            configuration.Bind("Kafka:consumer", consumerConfig);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new KafkaConsumer<TEvent>(consumerConfig);

            while (!stoppingToken.IsCancellationRequested)
            {
                var @event = consumer.ReadMessage(TimeSpan.FromMilliseconds(5000));

                var handler = serviceProvider.GetService<IEventHandler<TEvent>>();

                await handler.HandleAsync(@event);
            }

        }
    }
}
