using Confluent.Kafka;
using System;
using System.Reflection;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Extensions;

namespace TrashRouting.Common.Kafka
{
    public class KafkaConsumer<TEvent> where TEvent : IEvent
    {
        private readonly IConsumer<string, TEvent> consumer;

        public KafkaConsumer(ConsumerConfig consumerConfig)
        {
            var consumerBuilder = new ConsumerBuilder<string, TEvent>(consumerConfig);

            consumer = consumerBuilder.Build();

            consumer.Subscribe(GetTopicName());
        }

        public TEvent ReadMessage(TimeSpan timeout)
        {
            var consumeResult = consumer.Consume(timeout);
            return consumeResult.Value;
        }

        public static string GetTopicName()
        {
            var @namespace = typeof(TEvent).GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace;
            var separatedNamespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return $"{separatedNamespace}{typeof(TEvent).Name.Underscore()}".ToLowerInvariant();
        }
    }
}
