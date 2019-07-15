using System;

namespace TrashRouting.Common.Messaging
{
    public class CorrelationContext : ICorrelationContext
    {
        public Guid Id { get; }

        public Guid UserId { get; }

        public Guid ResourceId { get; }

        public string ConnectionId { get; }

        public string Name { get; }

        public string Origin { get; }

        public string Resource { get;  }

        public string Culture { get; }

        public DateTime CreatedAt { get; }

        public int Retries { get; }

        public CorrelationContext()
        {
            Id = Guid.NewGuid();
        }

        public CorrelationContext(Guid id)
        {
            Id = id;
        }

        public static CorrelationContext Create()
            => new CorrelationContext();

        public static CorrelationContext Create(Guid correlationId)
            => new CorrelationContext(correlationId);

        public static CorrelationContext Create(string correlationId)
           => new CorrelationContext(Guid.Parse(correlationId));
    }
}
