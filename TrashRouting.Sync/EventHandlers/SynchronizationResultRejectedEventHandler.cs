using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.EventHandlers
{
    public class SynchronizationResultRejectedEventHandler : IEventHandler<SynchronizationResultRejectedEvent>
    {
        private readonly ILogger<SynchronizationResultRejectedEventHandler> logger;

        public Task HandleAsync(SynchronizationResultRejectedEvent @event)
            => HandleAsync(@event, CorrelationContext.Create());

        public async Task HandleAsync(SynchronizationResultRejectedEvent @event, ICorrelationContext context)
        {
            logger.LogInformation($"Synchronization results accepted by {@event.AcceptedById} with reason: {@event.Reason}");
            // some async actions
        }
    }
}
