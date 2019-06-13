using System.Threading.Tasks;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Common.RabbitMQ
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context) 
            where TCommand : ICommand;
    }
}
