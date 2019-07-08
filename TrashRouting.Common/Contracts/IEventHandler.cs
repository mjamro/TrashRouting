using System.Threading.Tasks;
using TrashRouting.Common.Messaging;

namespace TrashRouting.Common.Contracts
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);

        Task HandleAsync(TEvent @event, ICorrelationContext context);
    }
}
