using System;
using System.Threading.Tasks;
using TrashRouting.Cluster.Commands;
using TrashRouting.Common.Contracts;
using TrashRouting.Common.Messaging;
using TrashRouting.Common.RabbitMQ;

namespace TrashRouting.Cluster.CommandHandlers
{
    public class ScheduleClusterAlgorithmCommandHandler : ICommandHandler<ScheduleClusterAlgorithmCommand>
    {
        public Task HandleAsync(ScheduleClusterAlgorithmCommand command)
        {
            throw new NotImplementedException();
        }

        public Task HandleAsync(ScheduleClusterAlgorithmCommand command, ICorrelationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
