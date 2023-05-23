using System;
using UniRx;

namespace Services.LevelProgressionService
{
    public interface ILevelProgressionService : IDisposable
    {
        IReadOnlyReactiveProperty<int> CustomersCount { get; }
        IReadOnlyReactiveProperty<TimeSpan> CurrentTime { get; }
        void CompleteCustomerOrder();
    }
}