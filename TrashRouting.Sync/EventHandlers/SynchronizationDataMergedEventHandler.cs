using System;
using System.Threading.Tasks;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.EventHandlers
{
    public class SynchronizationDataMergedEventHandler : IEventHandler<SynchronizationDataMergedEvent>
    {
        public Task HandleAsync(SynchronizationDataMergedEvent @event)
            => HandleAsync(@event, CorrelationContext.Create());

        public Task HandleAsync(SynchronizationDataMergedEvent @event, ICorrelationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
