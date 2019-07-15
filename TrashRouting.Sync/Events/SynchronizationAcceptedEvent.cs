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
        public DateTime AcceptedDate { get; }

        public SynchronizationAcceptedEvent(string acceptedById, string message, DateTime acceptedDate)
        {
            AcceptedById = acceptedById;
            Message = message;
            AcceptedDate = acceptedDate;
        }
    }
}
