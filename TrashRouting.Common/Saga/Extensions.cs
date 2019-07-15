using Autofac;
using System;
using System.Linq;
using System.Reflection;
using TrashRouting.Common.Contracts;

namespace TrashRouting.Common.Saga
{
    public static class Extensions
    {
        private static readonly Type[] SagaTypes = Assembly.GetExecutingAssembly()
           .GetTypes()
           .Where(t => t.IsAssignableTo<ISaga>())
           .ToArray();

        public static bool BelongsToSaga<TMessage>(this TMessage _) where TMessage : IEvent
            => SagaTypes.Any(t => t.IsAssignableTo<ISagaEvent<TMessage>>());
    }
}
