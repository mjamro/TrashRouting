using Newtonsoft.Json;
using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Commands
{
    [MessageNamespace("sync")]
    public class ScheduleSynchronizationCommand : ICommand
    {
        public string RequestedById { get; }
        public DateTime RunDate { get; }
        public string Message { get; }

        [JsonConstructor]
        public ScheduleSynchronizationCommand(string requestesById, DateTime runDate, string message)
        {
            RequestedById = requestesById;
            RunDate = runDate;
            Message = message;
        }
    }
}
