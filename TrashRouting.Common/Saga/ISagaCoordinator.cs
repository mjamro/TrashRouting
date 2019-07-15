using System;
using System.Threading.Tasks;

namespace TrashRouting.Common.Saga
{
    public interface ISagaCoordinator
    {
        Task ProcessAsync<TMessage>(
             TMessage message,
             ISagaContext context,
             Func<TMessage, Task> onCompleted = null,
             Func<TMessage, Task> onRejected = null);
    }
}
