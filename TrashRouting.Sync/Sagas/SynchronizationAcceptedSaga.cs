using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.RabbitMQ;
using TrashRouting.Common.Saga;
using TrashRouting.Sync.Commands;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.Sagas
{
    public class SynchronizationAcceptedSaga : Saga,
        ISagaInitiateEvent<SynchronizationAcceptedEvent>,
        ISagaEvent<SynchronizationDataMergedEvent>
    {
        private readonly ILogger<SynchronizationAcceptedSaga> logger;
        private readonly IBusPublisher busPublisher;

        public SynchronizationAcceptedSaga(ILogger<SynchronizationAcceptedSaga> logger, IBusPublisher busPublisher)
        {
            this.logger = logger;
            this.busPublisher = busPublisher;
        }

        public Task CompensateAsync(SynchronizationAcceptedEvent @event, ISagaContext sagaContext)
        {
            return Task.CompletedTask;
        }

        public Task CompensateAsync(SynchronizationDataMergedEvent @event, ISagaContext sagaContext)
        {
            return Task.CompletedTask;
        }

        public async Task HandleAsync(SynchronizationAcceptedEvent @event, ISagaContext sagaContext)
        {
            logger.LogInformation($"{nameof(@event)} {sagaContext.CorrelationId}");

            await busPublisher.SendAsync(
                new MergeSynchronizationDataCommand(), 
                CorrelationContext.Create(sagaContext.CorrelationId));
        }

        public Task HandleAsync(SynchronizationDataMergedEvent @event, ISagaContext sagaContext)
        {
            logger.LogInformation($"{nameof(@event)} {sagaContext.CorrelationId}");

            // send email notification command
            return Task.CompletedTask;
        }

    }
}
