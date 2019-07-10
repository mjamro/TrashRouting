using System;
using System.Collections.Generic;
using System.Text;

namespace TrashRouting.Common.Saga
{
    public interface ISagaEvent<TEvent>
    {
        Task HandleAsync(TEvent, ISagaContext sagaContext)
    }
}
