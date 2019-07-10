using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.EventHandlers
{
    public class SynchronizationResultAcceptedEventHandler : IEventHandler<SynchronizationResultAcceptedEvent>
    {
        private readonly ILogger<SynchronizationResultAcceptedEventHandler> logger;

        public SynchronizationResultAcceptedEventHandler(ILogger<SynchronizationResultAcceptedEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task HandleAsync(SynchronizationResultAcceptedEvent @event)
            => HandleAsync(@event, CorrelationContext.Create());

        public async Task HandleAsync(SynchronizationResultAcceptedEvent @event, ICorrelationContext context)
        {
            logger.LogInformation($"Synchronization results accepted by {@event.AcceptedById}");
            // some async actions
        }
    }
}
