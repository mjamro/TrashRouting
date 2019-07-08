using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Common.Kafka
{
    public class KafkaConsumer : IEventConsumer
    {
        private readonly IConsumer<string, string> consumer;
        private readonly IServiceProvider serviceProvider;

        public KafkaConsumer(IApplicationBuilder app)
        {
            serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            consumer = serviceProvider.GetService<IConsumer<string, string>>();
        }

        public async Task<IEventConsumer> ConsumeAsync<TEvent>() where TEvent : IEvent
        {
            var @event = consumer.Consume();

            var handler = serviceProvider.GetService<IEventHandler<TEvent>>();

            await handler.HandleAsync(JsonConvert.DeserializeObject<TEvent>(@event.Value));

            return this;
        }
    }
}
