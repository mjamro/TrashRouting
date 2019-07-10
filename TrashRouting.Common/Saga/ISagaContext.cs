using System;
using System.Collections.Generic;

namespace TrashRouting.Common.Saga
{
    public interface ISagaContext
    {
        string SagaId { get; }
        IDictionary<string, object> Metadata { get; }
        IEnumerable<Exception> Errors { get; }
    }
}
