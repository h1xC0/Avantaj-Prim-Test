using BaseInfrastructure;
using Constants;
using Services.AnimationService;
using Services.PlayerProgression;
using Signals;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;
using UniRx;

namespace GameState
{
    public class WinStatePresenter : BasePresenter<ILevelEndView>
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPlayerProgressionService _playerProgressionService;

        public WinStatePresenter(
            ILevelEndView viewContract,
            ICommandDispatcher commandDispatcher,
            IPlayerProgressionService playerProgressionService,
            IAnimationService animationService) : base(viewContract)
        {
            _commandDispatcher = commandDispatcher;
            _playerProgressionService = playerProgressionService;

            View.Construct(animationService);

            View.OnNextLevelButtonPressed
                .Subscribe(LoadNextLevel)
                .AddTo(CompositeDisposable);
            
            View.AnimateEnter();
        }

        private void LoadNextLevel(Unit args)
        {           
            _playerProgressionService.IncreaseLevelIndex();
            _commandDispatcher.Dispatch<LoadNextLevelSignal>(new SceneNamePayload(SceneNames.Gameplay));
        }
    }
}