using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;

namespace TrashRouting.Common.RabbitMQ
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient busClient;

        public BusPublisher(IBusClient busClient)
        {
            this.busClient = busClient;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context) where TEvent : IEvent
            => await busClient.PublishAsync(@event, ctx => ctx.UseMessageContext(context));

        //TODO: Add correlation Context
        public async Task SendAsync<TCommand>(TCommand command, ICorrelationContext context) where TCommand : ICommand
            => await busClient.PublishAsync(command, ctx => ctx.UseMessageContext(context));
    }
}
