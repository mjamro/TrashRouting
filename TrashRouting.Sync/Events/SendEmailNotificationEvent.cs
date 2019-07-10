using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Events
{
    [MessageNamespace("notification")]
    public class SendEmailNotificationEvent : IEvent
    {
        public string[] ReceiversIds { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}
