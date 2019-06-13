using System;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Commands
{
    public class ScheduleSynchronizationCommand : ICommand
    {
        public string RequestesById { get; set; }
        public DateTime RunDate { get; set; }
        public string Message { get; set; }
    }
}
