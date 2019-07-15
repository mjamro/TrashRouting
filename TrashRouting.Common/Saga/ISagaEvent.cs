using System.Threading.Tasks;

namespace TrashRouting.Common.Saga
{
    public interface ISagaEvent<TEvent>
    {
        Task HandleAsync(TEvent @event, ISagaContext sagaContext);
        Task CompensateAsync(TEvent @event, ISagaContext sagaContext);
    }
}
