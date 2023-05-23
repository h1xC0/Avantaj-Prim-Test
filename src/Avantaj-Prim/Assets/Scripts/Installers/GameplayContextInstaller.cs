using Commands;
using Services.AnimationService;
using Services.InputService;
using Services.LevelConfigurationService;
using Services.LevelProgressionService;
using Services.PlayerProgression;
using Services.PresenterProvider;
using Signals;
using Systems.CommandSystem;

namespace Installers
{
    public class GameplayContextInstaller : BaseInstaller
    {
        private readonly ICommandBinder _commandBinder;
        private readonly ICommandDispatcher _commandDispatcher;

        public GameplayContextInstaller(ICommandBinder commandBinder, ICommandDispatcher commandDispatcher) : base(commandBinder)
        {
            _commandBinder = commandBinder;
            _commandDispatcher = commandDispatcher;
        }
        
        public override void InstallBindings()
        {
            InstallCommands(_commandBinder);
            InstallServices();
        }

        protected override void InstallCommands(ICommandBinder commandBinder)
        {
            if (_commandDispatcher.HasListener(typeof(SetupGameplaySignal))) return;
            
            commandBinder.Bind<SetupGameplaySignal>()
                .To<SetupGameplayCommand>();

            commandBinder.Bind<EndLevelSignal>()
                .To<CreateLevelEndViewCommand>();

            commandBinder.Bind<LoadNextLevelSignal>()
                .To<DisposeLevelStateCommand>()
                .To<UnloadSceneCommand>()
                .To<LoadSceneCommand>();
        }

        protected override void InstallServices()
        {
            Container
                .BindInterfacesTo<InputService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<AnimationService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<PresenterContainerService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<PlayerProgressionService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesTo<LevelProgressionService>()
                .FromNew()
                .AsSingle();
        }
    }
}