using System;
using System.Collections.Generic;
using System.Text;

namespace TrashRouting.Common.Saga
{
    public interface ISagaState
    {
        string SagaId { get; }
        object Data { get; set; }
        IEnumerable<Exception> Errors { get; }
    }
}
