using System;
using System.Collections.Generic;
using System.Text;
using TrashRouting.Common.Enums;

namespace TrashRouting.Common.Saga
{
    public interface ISaga
    {
        string SagaId { get; }
        SagaState State { get; }
        void Initialize();
        void Complete();
        void Reject();
    }
}
