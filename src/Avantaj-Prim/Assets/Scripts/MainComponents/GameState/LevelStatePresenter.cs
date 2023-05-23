using BaseInfrastructure;
using Constants;
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

        public LevelStatePresenter(ILevelStateView view, IResourceProviderService resourceProviderService,
            ICommandDispatcher commandDispatcher, IPlayerProgressionService playerProgressionService) : base(view)
        {
            _resourceProviderService = resourceProviderService;
            _commandDispatcher = commandDispatcher;
            _playerProgressionService = playerProgressionService;
        }
        
        public void ConstructLeveEnd(bool gameState)
        {
            View.FadeBackground();

            var winView = _resourceProviderService.LoadResource<LevelEndView>(ResourceNames.WinView);
            var loseView = _resourceProviderService.LoadResource<LevelEndView>(ResourceNames.LoseView);
            
            var endLevelPanelView = gameState ? View.GetWinView(winView) : View.GetLoseView(loseView);
            endLevelPanelView.AnimateEnter();
            
            if (gameState)
                AddDisposable(new WinStatePresenter(endLevelPanelView, _commandDispatcher, _playerProgressionService));
            else
                AddDisposable(new LoseStatePresenter(endLevelPanelView, _commandDispatcher));
        }
    }
}