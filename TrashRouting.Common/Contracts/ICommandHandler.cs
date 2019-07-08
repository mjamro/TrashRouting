using System.Threading.Tasks;
using TrashRouting.Common.Messaging;

namespace TrashRouting.Common.Contracts
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);

        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}
