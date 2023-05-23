using BaseInfrastructure;
using Constants;
using MainComponents.GameState;
using Services.AnimationService;
using Services.PlayerProgression;
using Services.ResourceProvider;
using Systems.CommandSystem;

namespace GameState
{
    public class LevelStatePresenter : BasePresenter<ILevelStateView>
    {
        private readonly IResourceProviderService _resourceProviderService;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPlayerProgressionService _playerProgressionService;
        private readonly IAnimationService _animationService;

        public LevelStatePresenter(ILevelStateView view, IResourceProviderService resourceProviderService,
            ICommandDispatcher commandDispatcher, IPlayerProgressionService playerProgressionService, IAnimationService animationService) : base(view)
        {
            _resourceProviderService = resourceProviderService;
            _commandDispatcher = commandDispatcher;
            _playerProgressionService = playerProgressionService;
            _animationService = animationService;
        }
        
        public void ConstructLevelEnd(bool endLevelGameState)
        {
            View.FadeBackground();

            var winView = _resourceProviderService.LoadResource<LevelEndView>(ResourceNames.WinView);
            var loseView = _resourceProviderService.LoadResource<LevelEndView>(ResourceNames.LoseView);

            var endLevelPanelView = endLevelGameState ? View.GetWinView(winView) : View.GetLoseView(loseView);

            if (endLevelGameState)
            {
                AddDisposable(new WinStatePresenter(endLevelPanelView, _commandDispatcher, _playerProgressionService, _animationService));
            }
            else
            {
                AddDisposable(new LoseStatePresenter(endLevelPanelView, _commandDispatcher, _animationService));
            }
        }
    }
}