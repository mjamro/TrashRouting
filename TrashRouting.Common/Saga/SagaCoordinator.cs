using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TrashRouting.Common.Enums;

namespace TrashRouting.Common.Saga
{
    public class SagaCoordinator : ISagaCoordinator
    {
        private readonly IServiceProvider serviceProvider;

        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);

        public async Task ProcessAsync<TMessage>(
            TMessage message, 
            ISagaContext context, 
            Func<TMessage, Task> onCompleted = null, 
            Func<TMessage, Task> onRejected = null)
        {

            var sagaTasks = new List<Task>();
            var actions = serviceProvider.GetService<IEnumerable<ISagaEvent<TMessage>>>()
                .Union(serviceProvider.GetService<IEnumerable<ISagaInitiateEvent<TMessage>>>())
                .GroupBy(s => s.GetType())
                .Select(g => g.First())
                .Distinct()
                .ToList();


            foreach (var action in actions)
            {
                sagaTasks.Add(ProcessActionAsync(message, action, context, onCompleted, onRejected));
            }

            await Task.WhenAll(sagaTasks);
        }

        public async Task ProcessActionAsync<TMessage>(
            TMessage message, 
            ISagaEvent<TMessage> sagaEvent,
            ISagaContext context, 
            Func<TMessage, Task> onCompleted = null, 
            Func<TMessage, Task> onRejected = null)
        {
            var saga = (ISaga)sagaEvent;

            await Semaphore.WaitAsync();
            try
            {
                await sagaEvent.HandleAsync(message, context);
            }
            catch(Exception ex)
            {
                context.AddError(ex);
                saga.Reject();
            }
            finally
            {
                Semaphore.Release();
            }

            if(saga.State is SagaState.Rejected)
            {
                await onRejected(message);
                //compensate
            }
            else if (saga.State is SagaState.Completed)
            {
                await onCompleted(message);
            }

        }
    }
}
