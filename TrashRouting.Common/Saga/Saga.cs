using System;
using TrashRouting.Common.Enums;

namespace TrashRouting.Common.Saga
{
    public class Saga : ISaga
    {
        public string SagaId { get; set; }

        public SagaState State { get; protected set; }

        public void Initialize()
        {
            SagaId = Guid.NewGuid().ToString();
            State = SagaState.Pending;
        }

        public void Complete()
            => State = SagaState.Completed;

        public void Reject()
            => State = SagaState.Rejected;
    }
}
