using System;
using System.Threading.Tasks;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.RabbitMQ;
using TrashRouting.Sync.Commands;

namespace TrashRouting.Sync.CommandHandlers
{
    public class ScheduleSynchronizationCommandHandler : ICommandHandler<ScheduleSynchronizationCommand>
    {
        public Task HandleAsync(ScheduleSynchronizationCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(ScheduleSynchronizationCommand command, ICorrelationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
