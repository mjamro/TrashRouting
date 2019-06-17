using Newtonsoft.Json;
using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.API.Commands
{
    [MessageNamespace("sync")]
    public class ScheduleSynchronizationCommand : ICommand
    {
        public string RequestesById { get; set; }
        public DateTime RunDate { get; set; }
        public string Message { get; set; }

        [JsonConstructor]
        public ScheduleSynchronizationCommand(string requestesById, DateTime runDate, string message)
        {
            RequestesById = requestesById;
            RunDate = runDate;
            Message = message;
        }
    }
}
