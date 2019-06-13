using System;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Common.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string rabbitNamespace = null, string queueName = null,
            Func<TCommand> onError = null) where TCommand : ICommand;
    }
}
