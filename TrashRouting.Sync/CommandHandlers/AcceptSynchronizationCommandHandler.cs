using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.RabbitMQ;
using TrashRouting.Sync.Commands;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.CommandHandlers
{
    public class AcceptSynchronizationCommandHandler : ICommandHandler<AcceptSynchronizationCommand>
    {
        private readonly IBusPublisher busPublisher;
        private readonly ILogger<AcceptSynchronizationCommandHandler> logger;

        public AcceptSynchronizationCommandHandler(IBusPublisher busPublisher, ILogger<AcceptSynchronizationCommandHandler> logger)
        {
            this.busPublisher = busPublisher;
            this.logger = logger;
        }

        public Task HandleAsync(AcceptSynchronizationCommand command)
            => HandleAsync(command, CorrelationContext.Create());

        public async Task HandleAsync(AcceptSynchronizationCommand command, ICorrelationContext context)
        {
            logger.LogInformation($"{nameof(command)} {context.Id}");

            await busPublisher.PublishAsync(new SynchronizationAcceptedEvent(
                command.AcceptedById, command.Message, command.AcceptedDate), context);
        }
    }
}
