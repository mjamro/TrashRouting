using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.RabbitMQ;
using TrashRouting.Sync.Commands;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.CommandHandlers
{
    public class ScheduleSynchronizationCommandHandler : ICommandHandler<ScheduleSynchronizationCommand>
    {
        private readonly IBusPublisher busPublisher;
        private readonly ILogger<ScheduleSynchronizationCommandHandler> logger;

        public ScheduleSynchronizationCommandHandler(
            IBusPublisher busPublisher, 
            ILogger<ScheduleSynchronizationCommandHandler> logger)
        {
            this.busPublisher = busPublisher;
            this.logger = logger;
        }

        public async Task HandleAsync(ScheduleSynchronizationCommand command, ICorrelationContext context)
        {
            logger.LogInformation($"{nameof(command)} ({context.Id})");

            await busPublisher.PublishAsync(
                  new SynchronizationScheduledEvent(
                      command.RequestedById,
                      command.RunDate,
                      command.Message),
                  context);
        }

        public Task HandleAsync(ScheduleSynchronizationCommand command)
            => HandleAsync(command, CorrelationContext.Create());
    }
}
