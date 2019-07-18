using System;
using TrashRouting.Common.Attributes;
using TrashRouting.Common.Contracts;

namespace TrashRouting.API.Commands
{
    [MessageNamespace("sync")]
    public class AcceptSynchronizationCommand : ICommand
    {
        public string AcceptedById { get; }
        public DateTime AcceptedDate { get; }
        public int SynchronizationId { get; }
        public string Message { get; }

        public AcceptSynchronizationCommand(string acceptedById, DateTime acceptedDate, int synchronizationId, string message)
        {
            AcceptedById = acceptedById;
            AcceptedDate = acceptedDate;
            SynchronizationId = synchronizationId;
            Message = message;
        }

    }
}
