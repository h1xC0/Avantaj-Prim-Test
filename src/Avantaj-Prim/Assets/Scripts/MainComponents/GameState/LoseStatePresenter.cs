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
    public class LoseStatePresenter : BasePresenter<ILevelEndView>
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public LoseStatePresenter(
            ILevelEndView viewContract,
            ICommandDispatcher commandDispatcher, 
            IAnimationService animationService) : base(viewContract)
        {
            _commandDispatcher = commandDispatcher;

            View.Construct(animationService);

            View.OnNextLevelButtonPressed
                .Subscribe(RestartLevel)
                .AddTo(CompositeDisposable);

            View.AnimateEnter();
        }

        private void RestartLevel(Unit args)
        {           
            _commandDispatcher.Dispatch<LoadNextLevelSignal>(new SceneNamePayload(SceneNames.Gameplay));
        }
        
    }
}