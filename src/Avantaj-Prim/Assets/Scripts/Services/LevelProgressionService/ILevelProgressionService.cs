using System;
using UniRx;

namespace Services.LevelProgressionService
{
    public interface ILevelProgressionService : IDisposable
    {
        bool LevelEnd { get; }
        void SetLevelEnded(bool flag);
        
        IReadOnlyReactiveProperty<int> CustomersCount { get; }
        IReadOnlyReactiveProperty<TimeSpan> CurrentTime { get; }
        void CompleteCustomerOrder();
    }
}