using GameState;
using Services.PresenterProvider;
using Systems.CommandSystem;
using Systems.CommandSystem.Payloads;

namespace Commands
{
    public class CreateLevelEndViewCommand : Command
    {
        private readonly IPresenterContainerService _presenterContainerService;

        public CreateLevelEndViewCommand(IPresenterContainerService presenterContainerService)
        {
            _presenterContainerService = presenterContainerService;
        }
        
        protected override void Execute(ICommandPayload payload)
        {
            Retain();

            var gameState = payload as EndLevelStatePayload;
            _presenterContainerService.Resolve<LevelStatePresenter>().ConstructLeveEnd(gameState?.LevelEnd ?? false);
            
            Release();
        }

    }
}