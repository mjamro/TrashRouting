using System;
using System.Collections.Generic;
using TrashRouting.Common.Messaging;

namespace TrashRouting.Common.Saga
{
    public class SagaContext : ISagaContext
    {
        public Guid CorrelationId { get; }  

        public IDictionary<string, object> Metadata { get; }

        public ICollection<Exception> Errors { get; }

        public SagaContext(Guid correlationId, IDictionary<string, object> metadata)
        {
            CorrelationId = correlationId;
            Metadata = metadata;
            Errors = new List<Exception>();
        }
        public SagaContext(Guid correlationId)
        {
            CorrelationId = correlationId;
            Metadata = new Dictionary<string, object>();
            Errors = new List<Exception>();
        }

        public static ISagaContext Create(Guid correlationId, IDictionary<string, object> metadata)
            => new SagaContext(correlationId, metadata);

        public static ISagaContext CreateFromCorrelationContext(ICorrelationContext context)
            => new SagaContext(context.Id);

        public void AddError(Exception ex)
            => Errors.Add(ex);
    }
}
