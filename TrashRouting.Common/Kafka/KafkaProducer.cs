using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;

namespace TrashRouting.Common.Kafka
{
    public class KafkaProducer : IEventProducer
    {
        private readonly IProducer<string, string> producer;

        public KafkaProducer(IProducer<string, string> producer)
        {
            this.producer = producer;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context) where TEvent : IEvent
        {
            var message = new Message<string, string>()
            {
                Key = Guid.NewGuid().ToString(),
                Value = JsonConvert.SerializeObject(@event)
            };
            await producer.ProduceAsync(nameof(@event), message);
        }
    }
}
