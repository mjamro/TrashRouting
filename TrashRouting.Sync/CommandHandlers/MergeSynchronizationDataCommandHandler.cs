using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.RabbitMQ;
using TrashRouting.Sync.Commands;
using TrashRouting.Sync.Events;

namespace TrashRouting.Sync.CommandHandlers
{
    public class MergeSynchronizationDataCommandHandler : ICommandHandler<MergeSynchronizationDataCommand>
    {
        private readonly IBusPublisher busPublisher;
        private readonly ILogger<MergeSynchronizationDataCommandHandler> logger;

        public MergeSynchronizationDataCommandHandler(IBusPublisher busPublisher, ILogger<MergeSynchronizationDataCommandHandler> logger)
        {
            this.busPublisher = busPublisher;
            this.logger = logger;
        }

        public Task HandleAsync(MergeSynchronizationDataCommand command)
            => HandleAsync(command, CorrelationContext.Create());

        public async Task HandleAsync(MergeSynchronizationDataCommand command, ICorrelationContext context)
        {
            logger.LogInformation($"{nameof(command)} ({context.Id})");

            await busPublisher.PublishAsync(new SynchronizationDataMergedEvent(), context);
        }
    }
}
