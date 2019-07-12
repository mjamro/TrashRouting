using System;
using System.Threading.Tasks;

namespace TrashRouting.Common.Saga
{
    public interface ISagaCoordinator
    {
        Task ProcessAsync<TMessage>(
             TMessage message,
             ISagaEvent<TMessage> sagaEvent,
             ISagaContext context,
             Func<TMessage, Task> onCompleted = null,
             Func<TMessage, Task> onRejected = null);
    }
}
