using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.Saga;

namespace TrashRouting.Common.RabbitMQ
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient busClient;
        private readonly ISagaCoordinator sagaCoordinator;

        public BusPublisher(IBusClient busClient, ISagaCoordinator sagaCoordinator)
        {
            this.busClient = busClient;
            this.sagaCoordinator = sagaCoordinator;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context) where TEvent : IEvent
        {
            if (@event.BelongsToSaga())
            {
                var sagaContext = SagaContext.CreateFromCorrelationContext(context);
                await sagaCoordinator.ProcessAsync(@event, sagaContext);
            }
            else
            {
                await busClient.PublishAsync(@event, ctx => ctx.UseMessageContext(context));
            }
        }

        public async Task SendAsync<TCommand>(TCommand command, ICorrelationContext context) where TCommand : ICommand
            => await busClient.PublishAsync(command, ctx => ctx.UseMessageContext(context));
    }
}
