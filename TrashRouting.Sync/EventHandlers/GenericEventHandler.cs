using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.Saga;

namespace TrashRouting.Sync.EventHandlers
{
    public class GenericEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        private readonly ISagaCoordinator sagaCoordinator;

        public GenericEventHandler(ISagaCoordinator sagaCoordinator)
        {
            this.sagaCoordinator = sagaCoordinator;
        }

        public Task HandleAsync(TEvent @event)
            => HandleAsync(@event, CorrelationContext.Create());

        public async Task HandleAsync(TEvent @event, ICorrelationContext context)
        {
            if(@event.BelongsToSaga())
            {
                var sagaContext = SagaContext.CreateFromCorrelationContext(context);
                await sagaCoordinator.ProcessAsync(@event, sagaContext);
            }
        }
    }
}
