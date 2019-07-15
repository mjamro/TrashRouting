using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Sync.Commands
{
    [MessageNamespace("sync")]
    public class AcceptSynchronizationCommand : ICommand
    {
        public string AcceptedById { get; }
        public DateTime AcceptedDate { get; }
        public string Message { get; }

        public AcceptSynchronizationCommand(string acceptedById, DateTime acceptedDate, string message)
        {
            AcceptedById = acceptedById;
            AcceptedDate = acceptedDate;
            Message = message;
        }
    }
}
