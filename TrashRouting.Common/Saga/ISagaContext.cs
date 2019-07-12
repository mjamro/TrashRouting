using System;
using System.Collections.Generic;

namespace TrashRouting.Common.Saga
{
    public interface ISagaContext
    {
        Guid CorrelationId { get; }
        IDictionary<string, object> Metadata { get; }
        ICollection<Exception> Errors { get; }
        void AddError(Exception ex);
    }
}
