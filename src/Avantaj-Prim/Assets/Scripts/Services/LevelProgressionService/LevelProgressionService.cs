using System;
using Services.LevelConfigurationService;
using Services.PlayerProgression;
using Signals;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;
using UniRx;

namespace Services.LevelProgressionService
{
    public class LevelProgressionService : ILevelProgressionService
    {
        public bool LevelEnd => _levelEnded;
        private bool _levelEnded;
        
        public IReadOnlyReactiveProperty<int> CustomersCount => _customersCount;
        public IReadOnlyReactiveProperty<TimeSpan> CurrentTime => _currentTime;

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ReactiveProperty<int> _customersCount;
        private readonly ReactiveProperty<TimeSpan> _currentTime;
        private readonly CompositeDisposable _compositeDisposable;

        private float _currentTimerValue = 0f;
        
        public LevelProgressionService(
            ILevelConfigurationService levelConfigurationService,
            IPlayerProgressionService playerProgressionService,
            ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _compositeDisposable = new CompositeDisposable();

            var levelConfig = levelConfigurationService.GetLevelConfiguration(playerProgressionService.CurrentLevel.Value);
            _currentTimerValue = levelConfig.OrderWaitingTime * levelConfig.CustomersCount;
            
            _customersCount = new ReactiveProperty<int>(levelConfig.CustomersCount);
            _currentTime = new ReactiveProperty<TimeSpan>(TimeSpan.FromSeconds(_currentTimerValue));

            Observable.Timer(TimeSpan.FromSeconds(1f))
                .RepeatSafe()
                .Subscribe(_ => UpdateTimer())
                .AddTo(_compositeDisposable);
        }

        public void SetLevelEnded(bool flag)
        {
            _levelEnded = flag;
        }

        public void CompleteCustomerOrder()
        {
            if (_customersCount.Value <= 1)
            {
                CompleteLevel(true);
            }
            
            _customersCount.Value -= 1;
        }

        private void UpdateTimer()
        {
            if (_currentTimerValue <= 0)
            {
                CompleteLevel(false);
                return;
            }

            _currentTimerValue -= 1f;
            _currentTime.Value = TimeSpan.FromSeconds(_currentTimerValue);
        }

        private void CompleteLevel(bool levelComplete)
        {
            _commandDispatcher.Dispatch<EndLevelSignal>(new EndLevelStatePayload(levelComplete, true));
            Dispose();
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}