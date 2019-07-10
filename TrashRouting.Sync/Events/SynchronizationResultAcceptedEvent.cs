using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Events
{
    [MessageNamespace("sync")]
    public class SynchronizationResultAcceptedEvent : IEvent
    {
        public string RequestedById { get; }
        public string AcceptedById { get; }
        public string Message { get; set; }
    }
}
