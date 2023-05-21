using Commands;
using Constants;
using GameState;
using MainComponents.Gameplay;
using Services;
using Services.AnimationService;
using Services.EventBus;
using Services.InputService;
using Services.PresenterProvider;
using Signals;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;
using UnityEngine;

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
            
            
            commandBinder.Bind<LoadNextLevelSignal>()
                .To<UnloadSceneCommand>()
                .To<LoadSceneCommand>();
        }

        protected override void InstallServices()
        {
            Container.BindInterfacesTo<InputService>()
                .FromNew()
                .AsSingle();

            Container.BindInterfacesTo<AnimationService>()
                .FromNew()
                .AsSingle();

            Container.BindInterfacesTo<ViewManagementFactory>()
                .FromNew()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<PresenterProviderService>()
                .FromNew()
                .AsSingle();
        }
    }
}