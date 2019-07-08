using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.EventHandlers
{
    public class SynchronizationScheduledEventHandler : IEventHandler<SynchronizationScheduledEvent>
    {
        private readonly ILogger<SynchronizationScheduledEventHandler> logger;

        public SynchronizationScheduledEventHandler(ILogger<SynchronizationScheduledEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task HandleAsync(SynchronizationScheduledEvent @event)
            => HandleAsync(@event, CorrelationContext.Create());

        public Task HandleAsync(SynchronizationScheduledEvent @event, ICorrelationContext context)
        {
            logger.LogInformation($"{nameof(@event)} handling");
            return Task.CompletedTask;
        }
    }
}
