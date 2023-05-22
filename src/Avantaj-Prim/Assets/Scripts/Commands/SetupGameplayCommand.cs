using MainComponents.Gameplay;
using Services;
using Services.PresenterProvider;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;

namespace Commands
{
    public class SetupGameplayCommand : Command
    {
        private readonly IPresenterProviderService _presenterProviderService;
        private readonly ILevelConfigurationService _levelConfigurationService;
        private readonly IViewManagementFactory _viewManagementFactory;

        public SetupGameplayCommand(IPresenterProviderService presenterProviderService, ILevelConfigurationService levelConfigurationService, IViewManagementFactory viewManagementFactory)
        {
            _presenterProviderService = presenterProviderService;
            _levelConfigurationService = levelConfigurationService;
            _viewManagementFactory = viewManagementFactory;
        }
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var setupGameplayPayload = payload as SetupGameplayPayload;
            var container = setupGameplayPayload.Container;
            
             
            var gamePlayView = container.InstantiatePrefabForComponent<IGameplayView>(setupGameplayPayload.GameplayView, setupGameplayPayload.SpawnPoint);
            _presenterProviderService.BindGameplayPresenter(container.Instantiate<GameplayPresenter>(new object[] {gamePlayView, setupGameplayPayload.Canvas}));

            _presenterProviderService.GameplayPresenter.ConstructGameplay(_levelConfigurationService.GetLevelConfiguration(1));
            
            setupGameplayPayload.SceneTransitionService.FadeOut();
            
            Release();
        }
    }
}