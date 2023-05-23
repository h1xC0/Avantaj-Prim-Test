using BaseInfrastructure;
using GameState;
using MainComponents.Gameplay;
using MainComponents.GameplayUI;
using Services.LevelConfigurationService;
using Services.PlayerProgression;
using Services.PresenterProvider;
using Services.Transitions;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;

namespace Commands
{
    public class SetupGameplayCommand : Command
    {
        private readonly IPresenterContainerService _presenterContainerService;
        private readonly ISceneTransitionService _sceneTransitionService;
        private readonly ILevelConfigurationService _levelConfigurationService;
        private readonly IPlayerProgressionService _playerProgressionService;

        public SetupGameplayCommand(IPresenterContainerService presenterContainerService, ISceneTransitionService sceneTransitionService, ILevelConfigurationService levelConfigurationService, IPlayerProgressionService playerProgressionService)
        {
            _presenterContainerService = presenterContainerService;
            _sceneTransitionService = sceneTransitionService;
            _levelConfigurationService = levelConfigurationService;
            _playerProgressionService = playerProgressionService;
        }
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var setupGameplayPayload = payload as SetupGameplayPayload;
            var container = setupGameplayPayload.Container;
            
             
            var gamePlayView = container.InstantiatePrefabForComponent<IGameplayView>(setupGameplayPayload.GameplayView, setupGameplayPayload.SpawnPoint);
            var levelStateView = container.InstantiatePrefabForComponent<ILevelStateView>(setupGameplayPayload.LevelStateView, setupGameplayPayload.SpawnPoint);
            var levelDataView = container.InstantiatePrefabForComponent<ILevelDataView>(setupGameplayPayload.LevelDataView, setupGameplayPayload.SpawnPoint);
            
            var gameplayPresenter = container.Instantiate<GameplayPresenter>(new object[] { gamePlayView, setupGameplayPayload.Canvas});
            var levelStatePresenter = container.Instantiate<LevelStatePresenter>(new object[] { levelStateView });
            var levelDataPresenter = container.Instantiate<LevelDataPresenter>(new object[] { levelDataView });
            
            _presenterContainerService.BindPresenter(gameplayPresenter);
            _presenterContainerService.BindPresenter(levelStatePresenter);
            _presenterContainerService.BindPresenter(levelDataPresenter);

            _presenterContainerService.Resolve<GameplayPresenter>().ConstructGameplay(_levelConfigurationService.GetLevelConfiguration(_playerProgressionService.CurrentLevel.Value));
            
            _presenterContainerService.Resolve<LevelDataPresenter>().Construct();

            _sceneTransitionService.FadeOut();
            
            Release();
        }
    }
}