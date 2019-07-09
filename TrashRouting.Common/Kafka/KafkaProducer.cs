using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;

namespace TrashRouting.Common.Kafka
{
    public class KafkaProducer : IEventProducer
    {
        private readonly ProducerConfig config;

        public KafkaProducer(IConfiguration configuration)
        {
            var config = new ProducerConfig();
            configuration.Bind(config);
        }

        public async Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context) where TEvent : IEvent
        {
            var producerBuilder = new ProducerBuilder<string, TEvent>(config);

            using (var producer = producerBuilder.Build())
            {
                var message = new Message<string, TEvent>()
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = @event
                };

                await producer.ProduceAsync(nameof(@event), message);
            }
        }
    }
}
