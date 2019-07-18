using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Events
{
    [MessageNamespace("sync")]
    public class SynchronizationAcceptedEvent : IEvent
    {
        public string AcceptedById { get; }
        public string Message { get; }
        public int SynchronizationId { get; }
        public DateTime AcceptedDate { get; }

        public SynchronizationAcceptedEvent(string acceptedById, DateTime acceptedDate, int synchronizationId, string message)
        {
            AcceptedById = acceptedById;
            AcceptedDate = acceptedDate;
            SynchronizationId = synchronizationId;
            Message = message;
        }
    }
}
