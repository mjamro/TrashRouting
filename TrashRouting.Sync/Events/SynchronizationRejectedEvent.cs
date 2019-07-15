using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Events
{
    [MessageNamespace("sync")]
    public class SynchronizationRejectedEvent : IEvent
    {
        public string RequestedById { get; }
        public string AcceptedById { get; }
        public string Reason { get; }
        public DateTime RejectedDate { get; }

        public SynchronizationRejectedEvent(string requestedById, string acceptedById, string reason, DateTime rejectedDate)
        {
            RequestedById = requestedById;
            AcceptedById = acceptedById;
            Reason = reason;
            RejectedDate = rejectedDate;
        }

    }
}
