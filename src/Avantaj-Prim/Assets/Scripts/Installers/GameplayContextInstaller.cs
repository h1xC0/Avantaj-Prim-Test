using Systems.CommandSystem;

namespace Installers
{
    public class GameplayContextInstaller : BaseInstaller
    {
        public GameplayContextInstaller(ICommandBinder commandBinder) : base(commandBinder)
        {
            
        }
        
        public override void InstallBindings()
        {
            
        }

        protected override void InstallCommands(ICommandBinder commandBinder)
        {
            
        }

        protected override void InstallServices()
        {
            
        }
        
        public void BindCommands()
        {
            // if (_dispatcher.HasListener(typeof(SetupGameplaySignal))) return;
            //
            // commandBinder.Bind<SetupGameplaySignal>()
            //     .To<SetupGameplayUICommand>();
            //
            // commandBinder.Bind<EndLevelSignal>()
            //     .To<DisposeGameplayCommand>()
            //     .To<ConstructEndLevelUICommand>();
            //
            // commandBinder.Bind<LoadNextLevelSignal>()
            //     .To<DisposeWinLoseCommand>()
            //     .To<UnloadSceneCommand>()
            //     .To<LoadSceneCommand>();
        }
    }
}