using Newtonsoft.Json;
using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Events
{
    [MessageNamespace("sync")]
    public class SynchronizationScheduledEvent : IEvent
    {
        public string RequestedById { get; }
        public DateTime SynchronizationStartDate { get; }
        public string MessageId { get; }

        [JsonConstructor]
        public SynchronizationScheduledEvent(string requestedById, DateTime synchronizationStartDate, string messageId)
        {
            RequestedById = requestedById;
            SynchronizationStartDate = synchronizationStartDate;
            MessageId = messageId;
        }
    }
}
