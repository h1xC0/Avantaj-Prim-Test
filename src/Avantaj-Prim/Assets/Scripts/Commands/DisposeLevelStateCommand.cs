using GameState;
using MainComponents.Gameplay;
using MainComponents.GameplayUI;
using Services.PresenterProvider;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;

namespace Commands
{
    public class DisposeLevelStateCommand : Command
    {
        private readonly IPresenterContainerService _presenterContainerService;

        public DisposeLevelStateCommand(IPresenterContainerService presenterContainerService)
        {
            _presenterContainerService = presenterContainerService;
        }
        
        protected override void Execute(ICommandPayload commandPayload)
        {
            Retain();
            
            _presenterContainerService.Dispose();
            
            Release();
        }
    }
}