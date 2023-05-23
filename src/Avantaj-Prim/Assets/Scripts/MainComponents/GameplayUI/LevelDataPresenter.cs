using BaseInfrastructure;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using UniRx;

namespace MainComponents.GameplayUI
{
    public class LevelDataPresenter : BasePresenter<ILevelDataView>
    {
        private readonly IPlayerProgressionService _playerProgressionService;
        private readonly ILevelProgressionService _levelProgressionService;

        public LevelDataPresenter(ILevelDataView viewContract, IPlayerProgressionService playerProgressionService, ILevelProgressionService levelProgressionService) : base(viewContract)
        {
            _playerProgressionService = playerProgressionService;
            _levelProgressionService = levelProgressionService;
        }

        public void Construct()
        {
            _playerProgressionService.SoftCurrency
                .Subscribe(View.SetPlayerCurrency)
                .AddTo(CompositeDisposable);
            
            View.SetPlayerCurrency(_playerProgressionService.SoftCurrency.Value);
            
            _levelProgressionService.CurrentTime
                .Subscribe(View.SetLevelTimer)
                .AddTo(CompositeDisposable);
            
            View.SetLevelTimer(_levelProgressionService.CurrentTime.Value);

            
            _levelProgressionService.CustomersCount
                .Subscribe(View.SetCustomersCount)
                .AddTo(CompositeDisposable);
            
            View.SetCustomersCount(_levelProgressionService.CustomersCount.Value);
        }
    }
}