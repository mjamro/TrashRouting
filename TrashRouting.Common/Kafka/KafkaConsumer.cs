using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using TrashRouting.Common.Contracts;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
