using MainComponents.Gameplay;
using Services;
using Services.LevelConfigurationService;
using Services.PlayerProgression;
using Services.PresenterProvider;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;

namespace Commands
{
    public class SetupGameplayCommand : Command
    {
        private readonly IPresenterProviderService _presenterProviderService;
        private readonly ILevelConfigurationService _levelConfigurationService;
        private readonly IPlayerProgressionService _playerProgressionService;

        public SetupGameplayCommand(IPresenterProviderService presenterProviderService, ILevelConfigurationService levelConfigurationService, IPlayerProgressionService playerProgressionService)
        {
            _presenterProviderService = presenterProviderService;
            _levelConfigurationService = levelConfigurationService;
            _playerProgressionService = playerProgressionService;
        }
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var setupGameplayPayload = payload as SetupGameplayPayload;
            var container = setupGameplayPayload.Container;
            
             
            var gamePlayView = container.InstantiatePrefabForComponent<IGameplayView>(setupGameplayPayload.GameplayView, setupGameplayPayload.SpawnPoint);
            _presenterProviderService.BindGameplayPresenter(container.Instantiate<GameplayPresenter>(new object[] {gamePlayView, setupGameplayPayload.Canvas}));

            _presenterProviderService.GameplayPresenter.ConstructGameplay(_levelConfigurationService.GetLevelConfiguration(_playerProgressionService.CurrentLevel.Value));
            
            setupGameplayPayload.SceneTransitionService.FadeOut();
            
            Release();
        }
    }
}