using System;

namespace TrashRouting.Common.Messaging
{
    public interface ICorrelationContext
    {
        Guid Id { get; }
        Guid UserId { get; }
        Guid ResourceId { get; }
        string ConnectionId { get; }
        string Name { get; }
        string Origin { get; }
        string Resource { get; }
        string Culture { get; }
        DateTime CreatedAt { get; }
        int Retries { get; }
    }
}
